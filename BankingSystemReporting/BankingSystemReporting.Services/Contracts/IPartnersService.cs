namespace BankingSystemReporting.Services.Contracts
{
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.DTOs.Export;
    using BankingSystemReporting.Services.DTOs.Import;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;

    public interface IPartnersService
    {
        Task<Result> CreateAsync(BankEntityDTO dto);
        Task<Partner?> GetByNameAsync(string name);
        Task<IEnumerable<PartnerDTO>> GetAllAsync();
        Task<Result> GetByIdAsync(int id);
        Task<Result> GetByQueryParametersAsync(PartnerQueryDTO dto);
    }
}
