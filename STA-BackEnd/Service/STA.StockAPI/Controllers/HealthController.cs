using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace STA.StockAPI.Controllers
{
    public class HealthController : DefaultApiController
    {
        public HealthController(ILogger<DefaultApiController> logger) : base(logger)
        {

        }

        [HttpGet]
        public ActionResult Get()
        {
            this._logger.LogInformation("Health Checked!");
            return Ok();
        }
    }
}