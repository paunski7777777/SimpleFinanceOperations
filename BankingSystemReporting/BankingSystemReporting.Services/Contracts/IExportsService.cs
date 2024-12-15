namespace BankingSystemReporting.Services.Contracts
{
    using BankingSystemReporting.Common.Models.API;

    public interface IExportsService
    {
        Task<Result> ExportPartnersToCSVAsync();
        Task<Result> ExportMerchantsToCSVAsync();
        Task<Result> ExportTransactionsToCSVAsync();
    }
}
