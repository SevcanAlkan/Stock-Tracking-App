using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STA.EventBus.Model;

namespace STA.AuthenticationAPI.Controllers
{
    public class AuthenticationController : DefaultApiController
    {
        public AuthenticationController(ILogger<DefaultApiController> logger) : base(logger)
        {

        }

        [AllowAnonymous]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            //var user = _userService.Authenticate(model.Username, model.Password);

            //if (user == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });

            //return Ok(user);

            return Ok();
        }
    }
}