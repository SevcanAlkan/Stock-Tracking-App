using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NGA.MonolithAPI.Controllers.V2
{
    [Authorize]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public abstract class DefaultApiController : ControllerBase
    {

        protected ILogger<DefaultApiController> _logger;

        public DefaultApiController(ILogger<DefaultApiController> logger)
        {
            this._logger = logger;
        }
    }
}