namespace BankingSystemReporting.Services
{
    using Microsoft.Extensions.Logging;

    using BankingSystemReporting.Common.Helpers;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.Contracts;

    using System.Net;
    using System.Text;

    public class ExportsService(ILogger<ExportsService> logger, IConvertersService convertersService, IPartnersService partnersService, IMerchantsService merchantsService, ITransactionsService transactionsService) : IExportsService
    {
        private readonly ILogger<ExportsService> logger = logger;
        private readonly IConvertersService convertersService = convertersService;
        private readonly IPartnersService partnersService = partnersService;
        private readonly IMerchantsService merchantsService = merchantsService;
        private readonly ITransactionsService transactionsService = transactionsService;

        public async Task<Result> ExportPartnersToCSVAsync()
            => await this.ExportToCSVAsync(this.partnersService.GetAllAsync);

        public async Task<Result> ExportMerchantsToCSVAsync()
            => await this.ExportToCSVAsync(this.merchantsService.GetAllAsync);

        public async Task<Result> ExportTransactionsToCSVAsync()
            => await this.ExportToCSVAsync(this.transactionsService.GetAllAsync);

        private async Task<Result> ExportToCSVAsync<T>(Func<Task<IEnumerable<T>>> getDtos)
        {
            try
            {
                IEnumerable<T> dtos = await getDtos();
                string csvData = this.convertersService.ConvertToCSV(dtos);
                byte[] fileContent = Encoding.UTF8.GetBytes(csvData);
                return new ResultModel(fileContent);
            }
            catch (Exception exception)
            {
                return ErrorsHandler.HandleResultError(this.logger, HttpStatusCode.BadRequest, exception.Message, nameof(ExportsService), typeof(T).Name + nameof(ExportToCSVAsync));
            }
        }
    }
}
