using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STA.EventBus;
using STA.EventBus.Model;

namespace STA.AuthenticationAPI.Controllers
{
    public class AuthenticationController : DefaultApiController
    {
        private MessageSender sender;

        public AuthenticationController(ILogger<DefaultApiController> logger, ConnectionBuilder builder) : base(logger)
        {
            this.sender = new MessageSender(builder);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            sender.Send<AuthenticateModel>(model, MessageQueue.UserValidation);
            //var user = _userService.Authenticate(model.Username, model.Password);

            //if (user == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });

            //return Ok(user);

            return Ok();
        }
    }
}