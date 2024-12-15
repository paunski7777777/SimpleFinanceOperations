namespace BankingSystemReporting.Services.Contracts
{
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;

    using ImportTransactionDTO = DTOs.Import.TransactionDTO;
    using ExportTransactionDTO = DTOs.Export.TransactionDTO;

    public interface ITransactionsService
    {
        Task<Result> CreateAsync(ImportTransactionDTO dto, int merchantId);
        Task<bool> ExistsByExternalIdAsync(string externalId);
        Task<IEnumerable<ExportTransactionDTO>> GetAllAsync();
        Task<Result> GetByIdAsync(int id);
        Task<Result> GetByQueryParametersAsync(TransactionQueryDTO dto);
    }
}
