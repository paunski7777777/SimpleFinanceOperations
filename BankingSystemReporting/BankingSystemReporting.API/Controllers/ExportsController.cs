namespace BankingSystemReporting.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BankingSystemReporting.API.Controllers.Base;
    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.Contracts;

    public class ExportsController(IExportsService exportsService) : BaseApiController
    {
        private readonly IExportsService exportsService = exportsService;

        [HttpGet(nameof(PartnersToCSV))]
        public async Task<IActionResult> PartnersToCSV()
            => await ExportToCSV(this.exportsService.ExportPartnersToCSVAsync, AppConstants.CSV.FileNames.Partners);

        [HttpGet(nameof(MerchantsToCSV))]
        public async Task<IActionResult> MerchantsToCSV()
            => await ExportToCSV(this.exportsService.ExportMerchantsToCSVAsync, AppConstants.CSV.FileNames.Merchants);

        [HttpGet(nameof(TransactionsToCSV))]
        public async Task<IActionResult> TransactionsToCSV()
            => await ExportToCSV(this.exportsService.ExportTransactionsToCSVAsync, AppConstants.CSV.FileNames.Transactions);

        private async Task<IActionResult> ExportToCSV(Func<Task<Result>> exportFunction, string fileName)
        {
            Result result = await exportFunction();

            if (result.Succeeded)
            {
                return this.File((byte[])result.Data.Value, AppConstants.FileFormats.CSV, fileName);
            }

            return this.GenericResponse(result);
        }
    }
}
