namespace BankingSystemReporting.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BankingSystemReporting.API.Controllers.Base;
    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.DTOs.Import;
    using BankingSystemReporting.Services.Contracts;

    public class ImportsController(IImportService importsService) : BaseApiController
    {
        private readonly IImportService importsService = importsService;

        [HttpPost]
        [Route(nameof(XMLTransactions))]
        [Consumes(AppConstants.FileFormats.XML)]
        public async Task<IActionResult> XMLTransactions([FromBody] OperationDTO dto)
        {
            Result result = await this.importsService.ImportXMLTransactionsAsync(dto);

            return this.GenericResponse(result);
        }
    }
}
