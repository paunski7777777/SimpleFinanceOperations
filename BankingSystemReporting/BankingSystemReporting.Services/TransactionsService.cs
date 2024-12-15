namespace BankingSystemReporting.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Data;
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services.Mapping;
    using BankingSystemReporting.Common.Extensions;
    using BankingSystemReporting.Common.Helpers;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;
    using BankingSystemReporting.Services.DTOs;
    using BankingSystemReporting.Models.Enums;

    using System.Net;

    using ImportTransactionDTO = DTOs.Import.TransactionDTO;
    using ExportTransactionDTO = DTOs.Export.TransactionDTO;

    public class TransactionsService(ILogger<TransactionsService> logger, AppDbContext dbContext) : ITransactionsService
    {
        private readonly ILogger<TransactionsService> logger = logger;
        private readonly AppDbContext dbContext = dbContext;

        public async Task<Result> CreateAsync(ImportTransactionDTO dto, int merchantId)
        {
            if (dto == null)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.BadRequest, string.Format(AppConstants.Messages.Errors.EmptyParameter, nameof(dto)), nameof(TransactionsService), nameof(CreateAsync));
            }

            try
            {
                Transaction transaction = AutoMapperConfig.MapperInstance.Map<Transaction>(dto);
                transaction.MerchantID = merchantId;

                await this.dbContext.Transactions.AddAsync(transaction);
                await this.dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.InternalServerError, exception.Message, nameof(TransactionsService), nameof(CreateAsync));
            }
        }

        public async Task<bool> ExistsByExternalIdAsync(string externalId)
            => await this.dbContext.Transactions
            .AsNoTracking()
            .AnyAsync(p => p.ExternalId.ToLower() == externalId.NormalizeForComparison());

        public async Task<IEnumerable<ExportTransactionDTO>> GetAllAsync()
            => await this.dbContext.Transactions
            .AsNoTracking()
            .To<ExportTransactionDTO>()
            .ToListAsync();

        public async Task<Result> GetByIdAsync(int id)
        {
            try
            {
                ExportTransactionDTO? dto = await this.dbContext.Transactions
                    .AsNoTracking()
                    .Where(p => p.Id == id)
                    .To<ExportTransactionDTO>()
                    .FirstOrDefaultAsync();

                if (dto == null)
                {
                    return new ErrorModel(HttpStatusCode.NotFound, string.Format(AppConstants.Messages.Errors.EntityByIDNotFound, nameof(Transaction), id));
                }

                return new ResultModel(dto);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(logger, HttpStatusCode.InternalServerError, exception.Message, nameof(TransactionsService), nameof(GetByIdAsync));
            }
        }

        public async Task<Result> GetByQueryParametersAsync(TransactionQueryDTO dto)
        {
            try
            {
                IQueryable<Transaction> query = this.dbContext.Transactions.AsNoTracking();

                query = BuildQuery(dto, query);

                int totalCount = await query.CountAsync();

                List<ExportTransactionDTO> transactions = await query
                    .ApplyPagination(dto.PageNumber, dto.PageSize)
                    .To<ExportTransactionDTO>()
                    .ToListAsync();

                PagedResult<ExportTransactionDTO> pagedResult = new(transactions, totalCount, dto.PageNumber, dto.PageSize);
                return new ResultModel(pagedResult);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(logger, HttpStatusCode.InternalServerError, exception.Message, nameof(TransactionsService), nameof(GetByQueryParametersAsync));
            }
        }

        private static IQueryable<Transaction> BuildQuery(TransactionQueryDTO dto, IQueryable<Transaction> query)
        {
            if (!string.IsNullOrWhiteSpace(dto.CreateDate) && DateTime.TryParse(dto.CreateDate, out DateTime createDate))
            {
                query = query.Where(t => t.CreateDate.Date == createDate.Date);
            }

            if (!string.IsNullOrWhiteSpace(dto.Direction) && Enum.TryParse(dto.Direction, true, out TransactionDirection direction))
            {
                query = query.Where(t => t.Direction == direction);
            }

            if (dto.Amount.HasValue)
            {
                query = query.Where(t => t.Amount == dto.Amount.Value);
            }

            if (!string.IsNullOrWhiteSpace(dto.Currency))
            {
                query = query.Where(t => t.Currency.ToLower() == dto.Currency.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.DebtorIBAN))
            {
                query = query.Where(t => t.DebtorIBAN.ToLower() == dto.DebtorIBAN.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.BeneficiaryIBAN))
            {
                query = query.Where(t => t.BeneficiaryIBAN.ToLower() == dto.BeneficiaryIBAN.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.Status) && Enum.TryParse(dto.Status, true, out TransactionStatus status))
            {
                query = query.Where(t => t.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(dto.ExternalId))
            {
                query = query.Where(t => t.ExternalId.ToLower() == dto.ExternalId.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.MerchantName))
            {
                query = query.Where(t => t.Merchant.Name.ToLower() == dto.MerchantName.NormalizeForComparison());
            }

            return query;
        }
    }
}
