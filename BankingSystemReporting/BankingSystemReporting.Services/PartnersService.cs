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

    public class PartnersService(ILogger<PartnersService> logger, AppDbContext dbContext) : IPartnersService
    {
        private readonly ILogger<PartnersService> logger = logger;
        private readonly AppDbContext dbContext = dbContext;

        public async Task<Result> CreateAsync(BankEntityDTO dto)
        {
            if (dto == null)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.BadRequest, string.Format(AppConstants.Messages.Errors.EmptyParameter, nameof(dto)), nameof(PartnersService), nameof(CreateAsync));
            }

            try
            {
                Partner partner = AutoMapperConfig.MapperInstance.Map<Partner>(dto);

                await this.dbContext.Partners.AddAsync(partner);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel(partner);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.InternalServerError, exception.Message, nameof(PartnersService), nameof(CreateAsync));
            }
        }

        public async Task<Partner?> GetByNameAsync(string name)
            => await this.dbContext.Partners
            .FirstOrDefaultAsync(p => p.Name.ToLower() == name.NormalizeForComparison());

        public async Task<IEnumerable<PartnerDTO>> GetAllAsync()
            => await this.dbContext.Partners
            .AsNoTracking()
            .To<PartnerDTO>()
            .ToListAsync();

        public async Task<Result> GetByIdAsync(int id)
        {
            try
            {
                PartnerDTO? dto = await this.dbContext.Partners
                    .AsNoTracking()
                    .Where(p => p.Id == id)
                    .To<PartnerDTO>()
                    .FirstOrDefaultAsync();

                if (dto == null)
                {
                    return new ErrorModel(HttpStatusCode.NotFound, string.Format(AppConstants.Messages.Errors.EntityByIDNotFound, nameof(Partner), id));
                }

                return new ResultModel(dto);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(logger, HttpStatusCode.InternalServerError, exception.Message, nameof(PartnersService), nameof(GetByIdAsync));
            }
        }

        public async Task<Result> GetByQueryParametersAsync(PartnerQueryDTO dto)
        {
            try
            {
                IQueryable<Partner> query = this.dbContext.Partners.AsNoTracking();

                query = BuildQuery(dto, query);

                int totalCount = await query.CountAsync();

                List<PartnerDTO> partners = await query
                    .ApplyPagination(dto.PageNumber, dto.PageSize)
                    .To<PartnerDTO>()
                    .ToListAsync();

                PagedResult<PartnerDTO> pagedResult = new(partners, totalCount, dto.PageNumber, dto.PageSize);
                return new ResultModel(pagedResult);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(logger, HttpStatusCode.InternalServerError, exception.Message, nameof(PartnersService), nameof(GetByQueryParametersAsync));
            }
        }

        private static IQueryable<Partner> BuildQuery(PartnerQueryDTO dto, IQueryable<Partner> query)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                query = query.Where(e => e.Name.ToLower() == dto.Name.NormalizeForComparison());
            }

            return query;
        }
    }
}
