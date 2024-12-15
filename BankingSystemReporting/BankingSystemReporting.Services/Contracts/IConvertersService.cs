namespace BankingSystemReporting.Services.Contracts
{
    public interface IConvertersService
    {
        string ConvertToCSV<T>(IEnumerable<T> data);
    }
}
