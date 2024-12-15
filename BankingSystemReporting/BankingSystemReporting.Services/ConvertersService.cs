namespace BankingSystemReporting.Services
{
    using CsvHelper.Configuration;
    using CsvHelper;

    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Common.Helpers;

    using Microsoft.Extensions.Logging;

    public class ConvertersService(ILogger<ConvertersService> logger, CsvConfiguration csvConfiguration) : IConvertersService
    {
        private readonly ILogger<ConvertersService> logger = logger;
        private readonly CsvConfiguration csvConfiguration = csvConfiguration;

        public string ConvertToCSV<T>(IEnumerable<T> data)
        {
            if (data == null || !data.Any())
            {
                ErrorsHandler.HandleArgumentNullException(this.logger, nameof(ConvertersService), nameof(ConvertToCSV), AppConstants.Messages.Errors.NoDataForExport);
            }

            try
            {
                using StringWriter writer = new();
                using CsvWriter csv = new(writer, this.csvConfiguration);

                csv.WriteRecords(data);

                return writer.ToString();
            }
            catch (Exception exception)
            {
                ErrorsHandler.HandleInvalidOperationError(this.logger, nameof(ConvertersService), nameof(ConvertToCSV), exception.Message);
                throw;
            }
        }
    }
}
