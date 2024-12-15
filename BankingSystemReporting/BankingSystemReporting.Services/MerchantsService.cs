namespace BankingSystemReporting.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Common.Extensions;
    using BankingSystemReporting.Common.Helpers;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Data;
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services.DTOs;
    using BankingSystemReporting.Services.DTOs.Export;
    using BankingSystemReporting.Services.DTOs.Import;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;
    using BankingSystemReporting.Services.Mapping;

    using System.Net;

    public class MerchantsService(ILogger<MerchantsService> logger, AppDbContext dbContext) : IMerchantsService
    {
        private readonly ILogger<MerchantsService> logger = logger;
        private readonly AppDbContext dbContext = dbContext;

        public async Task<Result> CreateAsync(BankEntityDTO dto, int partnerId)
        {
            if (dto == null)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.BadRequest, string.Format(AppConstants.Messages.Errors.EmptyParameter, nameof(dto)), nameof(MerchantsService), nameof(CreateAsync));
            }

            try
            {
                Merchant merchant = AutoMapperConfig.MapperInstance.Map<Merchant>(dto);
                merchant.PartnerID = partnerId;

                await this.dbContext.Merchants.AddAsync(merchant);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel(merchant);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.InternalServerError, exception.Message, nameof(MerchantsService), nameof(CreateAsync));
            }
        }

        public async Task<Merchant?> GetByNameAsync(string name)
            => await this.dbContext.Merchants
            .FirstOrDefaultAsync(p => p.Name.ToLower() == name.NormalizeForComparison());

        public async Task<IEnumerable<MerchantDTO>> GetAllAsync()
            => await this.dbContext.Merchants
            .AsNoTracking()
            .To<MerchantDTO>()
            .ToListAsync();

        public async Task<Result> GetByIdAsync(int id)
        {
            try
            {
                MerchantDTO? dto = await this.dbContext.Merchants
                    .AsNoTracking()
                    .Where(p => p.Id == id)
                    .To<MerchantDTO>()
                    .FirstOrDefaultAsync();

                if (dto == null)
                {
                    return new ErrorModel(HttpStatusCode.NotFound, string.Format(AppConstants.Messages.Errors.EntityByIDNotFound, nameof(Merchant), id));
                }

                return new ResultModel(dto);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(logger, HttpStatusCode.InternalServerError, exception.Message, nameof(MerchantsService), nameof(GetByIdAsync));
            }
        }

        public async Task<Result> GetByQueryParametersAsync(MerchantQueryDTO dto)
        {
            try
            {
                IQueryable<Merchant> query = this.dbContext.Merchants.AsNoTracking();

                query = BuildQuery(dto, query);

                int totalCount = await query.CountAsync();

                List<MerchantDTO> merchants = await query
                    .ApplyPagination(dto.PageNumber, dto.PageSize)
                    .To<MerchantDTO>()
                    .ToListAsync();

                PagedResult<MerchantDTO> pagedResult = new(merchants, totalCount, dto.PageNumber, dto.PageSize);
                return new ResultModel(pagedResult);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(logger, HttpStatusCode.InternalServerError, exception.Message, nameof(MerchantsService), nameof(GetByQueryParametersAsync));
            }
        }

        private static IQueryable<Merchant> BuildQuery(MerchantQueryDTO dto, IQueryable<Merchant> query)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                query = query.Where(e => e.Name.ToLower() == dto.Name.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.BoardingDate) && DateTime.TryParse(dto.BoardingDate, out DateTime boardingDate))
            {
                query = query.Where(m => m.BoardingDate.Date == boardingDate.Date);
            }

            if (!string.IsNullOrWhiteSpace(dto.URL))
            {
                query = query.Where(m => m.URL.ToLower() == dto.URL.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.Country))
            {
                query = query.Where(m => m.Country.ToLower() == dto.Country.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.Address1))
            {
                query = query.Where(m => m.Address1.ToLower() == dto.Address1.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.Address2))
            {
                query = query.Where(m => m.Address2.ToLower() == dto.Address2.NormalizeForComparison());
            }

            if (!string.IsNullOrWhiteSpace(dto.PartnerName))
            {
                query = query.Where(m => m.Partner.Name.ToLower() == dto.PartnerName.NormalizeForComparison());
            }

            return query;
        }
    }
}
