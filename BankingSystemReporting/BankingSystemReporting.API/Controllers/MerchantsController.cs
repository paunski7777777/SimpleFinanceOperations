namespace BankingSystemReporting.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BankingSystemReporting.API.Controllers.Base;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;

    public class MerchantsController(IMerchantsService merchantsService) : BaseApiController
    {
        private readonly IMerchantsService merchantsService = merchantsService;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Result result = await this.merchantsService.GetByIdAsync(id);
            return this.GenericResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByQuery([FromQuery] MerchantQueryDTO query)
        {
            Result result = await this.merchantsService.GetByQueryParametersAsync(query);
            return this.GenericResponse(result);
        }
    }
}
