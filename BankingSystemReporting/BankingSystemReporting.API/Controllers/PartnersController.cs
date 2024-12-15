namespace BankingSystemReporting.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BankingSystemReporting.API.Controllers.Base;
    using BankingSystemReporting.Common.Models.API;
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services.DTOs.QueryParamteres;

    public class PartnersController(IPartnersService partnersService) : BaseApiController
    {
        private readonly IPartnersService partnersService = partnersService;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Result result = await this.partnersService.GetByIdAsync(id);
            return this.GenericResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByQuery([FromQuery] PartnerQueryDTO query)
        {
            Result result = await this.partnersService.GetByQueryParametersAsync(query);
            return this.GenericResponse(result);
        }
    }
}
