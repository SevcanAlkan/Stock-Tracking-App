using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace STA.UserAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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