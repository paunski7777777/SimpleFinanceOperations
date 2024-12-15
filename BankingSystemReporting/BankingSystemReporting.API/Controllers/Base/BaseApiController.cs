namespace BankingSystemReporting.API.Controllers.Base
{
    using Microsoft.AspNetCore.Mvc;

    using BankingSystemReporting.Common.Models.API;

    using System.Net;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult GenericResponse(Result result)
        {
            if (result.Succeeded)
            {
                if (result.Data != null)
                {
                    return this.Ok(result.Data.Value);
                }

                return this.Ok();
            }

            return result.Error?.Status switch
            {
                HttpStatusCode.Unauthorized => this.Unauthorized(result.Error),
                HttpStatusCode.Forbidden => this.Forbid(),
                HttpStatusCode.NotFound => this.NotFound(result.Error),
                _ => this.BadRequest(result.Error),
            };
        }
    }
}
