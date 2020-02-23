using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STA.Core.Validation;
using STA.User.Data.Service;
using STA.User.Model.DTO;
using STA.User.Model.ViewModel;

namespace STA.UserAPI.Controllers
{
    public class UserController : DefaultApiController
    {
        private IUserService _service;

        readonly UserManager<UserDTO> _userManager;

        public UserController(IUserService service,
            UserManager<UserDTO> userManager,
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
                var result = _service.GetAll();

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

                var user = await _userManager.FindByIdAsync(id.ToString());
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