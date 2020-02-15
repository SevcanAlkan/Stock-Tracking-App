using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGA.Core.Validation;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Threading.Tasks;

namespace NGA.MonolithAPI.Controllers.V2
{
    public class UserController : DefaultApiController
    {
        private IUserService _service;

        readonly UserManager<User> _userManager;

        public UserController(IUserService service,
            UserManager<User> userManager,
            ILogger<UserController> logger)
             : base(logger)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet("UserNameIsExist/{userName}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public ActionResult UserNameIsExist(string userName)
        {
            try
            {
                var result = _service.Any(userName);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public ActionResult Get()
        {
            try
            {
                var result = _service.GetUserList();

                if (result.IsNull())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public ActionResult GetById(Guid id)
        {
            try
            {
                if (id.IsNull())
                    return BadRequest();

                var result = _service.GetById(id);

                if (result.IsNull())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, [FromBody]UserUpdateVM model)
        {
            try
            {
                if (id.IsNull())
                    return BadRequest();

                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user.IsNull())
                    return NotFound();

                user.About = model.About;
                user.DisplayName = model.DisplayName;
                user.IsAdmin = model.IsAdmin;
                user.UserName = model.UserName;

                IdentityResult result;

                if (model.OldPassword != model.PasswordHash)
                {
                    result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.PasswordHash);
                    if (!result.Succeeded)
                        return Conflict();
                }
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return Conflict();


                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }
    }
}
