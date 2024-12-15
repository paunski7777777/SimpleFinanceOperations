namespace BankingSystemReporting.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BankingSystemReporting.API.Controllers.Base;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;

    public class TransactionsController(ITransactionsService transactionsService) : BaseApiController
    {
        private readonly ITransactionsService transactionsService = transactionsService;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Result result = await this.transactionsService.GetByIdAsync(id);
            return this.GenericResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByQuery([FromQuery] TransactionQueryDTO query)
        {
            Result result = await this.transactionsService.GetByQueryParametersAsync(query);
            return this.GenericResponse(result);
        }
    }
}
