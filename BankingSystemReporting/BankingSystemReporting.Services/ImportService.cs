namespace BankingSystemReporting.Services
{
    using Microsoft.Extensions.Logging;

    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Common.Helpers;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services.DTOs.Import;

    using System.Net;

    public class ImportService(ILogger<ImportService> logger, IPartnersService partnersService, IMerchantsService merchantsService, ITransactionsService transactionsService) : IImportService
    {
        private readonly ILogger<ImportService> logger = logger;
        private readonly IPartnersService partnersService = partnersService;
        private readonly IMerchantsService merchantsService = merchantsService;
        private readonly ITransactionsService transactionsService = transactionsService;

        public async Task<Result> ImportXMLTransactionsAsync(OperationDTO dto)
        {
            if (dto == null || dto.Transactions == null || dto.Transactions.Length == 0)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.BadRequest, AppConstants.Messages.Errors.InvalidXMLData, nameof(ImportService), nameof(ImportXMLTransactionsAsync));
            }

            List<Result> results = [];

            foreach (TransactionDTO transactionDto in dto.Transactions)
            {
                try
                {
                    Partner? partner = await this.EnsurePartnerAsync(transactionDto.Debtor);
                    Merchant? merchant = await this.EnsureMerchantAsync(transactionDto.Beneficiary, partner.Id);

                    await this.EnsureTransactionAsync(transactionDto, merchant.Id);

                    results.Add(true);
                }
                catch (Exception exception)
                {
                    ErrorsHandler.HandleResultError(results, this.logger, HttpStatusCode.BadRequest, string.Format(AppConstants.Messages.Errors.ErrorProcessingTransaction, transactionDto.ExternalId, exception.Message));
                }
            }

            return this.ReturnResult(results);
        }

        private async Task<Partner?> EnsurePartnerAsync(BankEntityDTO dto)
        {
            Partner? partner = await this.partnersService.GetByNameAsync(dto.BankName);
            if (partner == null)
            {
                Result partnerCreationResult = await this.partnersService.CreateAsync(dto);
                EnsureSuccess(partnerCreationResult);
                partner = (Partner?)partnerCreationResult.Data?.Value;
            }

            return partner;
        }

        private async Task<Merchant?> EnsureMerchantAsync(BankEntityDTO dto, int partnerId)
        {
            Merchant? merchant = await this.merchantsService.GetByNameAsync(dto.BankName);
            if (merchant == null)
            {
                Result merchantCreationResult = await this.merchantsService.CreateAsync(dto, partnerId);
                EnsureSuccess(merchantCreationResult);
                merchant = (Merchant?)merchantCreationResult.Data?.Value;
            }

            return merchant;
        }

        private async Task EnsureTransactionAsync(TransactionDTO dto, int merchantId)
        {
            bool transactionExists = await this.transactionsService.ExistsByExternalIdAsync(dto.ExternalId);
            if (!transactionExists)
            {
                Result transactionCreationResult = await this.transactionsService.CreateAsync(dto, merchantId);
                EnsureSuccess(transactionCreationResult);
            }
        }

        private Result ReturnResult(List<Result> results)
        {
            bool success = results.All(r => r.Succeeded);
            if (success)
            {
                return success;
            }
            else
            {
                string?[] errors = results
                    .Where(r => !r.Succeeded)
                    .Select(r => r.Error?.Errors?.ToString())
                    .ToArray();

                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.BadRequest, string.Join(AppConstants.ErrorSeparator, errors), nameof(ImportService), nameof(ReturnResult));
            }
        }

        private static void EnsureSuccess(Result result)
        {
            if (!result.Succeeded)
            {
                throw new Exception(result.Error?.Errors?.ToString());
            }
        }
    }
}
