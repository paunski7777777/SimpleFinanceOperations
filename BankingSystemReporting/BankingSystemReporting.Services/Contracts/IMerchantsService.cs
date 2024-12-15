namespace BankingSystemReporting.Services.Contracts
{
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.DTOs.Export;
    using BankingSystemReporting.Services.DTOs.Import;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;

    public interface IMerchantsService
    {

        Task<Result> CreateAsync(BankEntityDTO dto, int partnerId);
        Task<Merchant?> GetByNameAsync(string name);
        Task<IEnumerable<MerchantDTO>> GetAllAsync();
        Task<Result> GetByIdAsync(int id);
        Task<Result> GetByQueryParametersAsync(MerchantQueryDTO dto);
    }
}
