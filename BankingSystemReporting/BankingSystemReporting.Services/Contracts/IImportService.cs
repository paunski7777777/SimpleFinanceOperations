namespace BankingSystemReporting.Services.Contracts
{
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.DTOs.Import;

    public interface IImportService
    {
        Task<Result> ImportXMLTransactionsAsync(OperationDTO dto);
    }
}
