using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NGA.Core.Validation;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NGA.MonolithAPI.Controllers.V2
{
    public class UserController : DefaultApiController
    {
        private IConfiguration _config;
        private IUserService _service;
        private readonly IMapper _mapper;

        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public UserController(IUserService service, IConfiguration config, UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper, ILogger<UserController> logger)
             : base(logger)
        {
            _config = config;
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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
        public async Task<ActionResult> Update(Guid id, UserUpdateVM model)
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

        [HttpGet("CreateToken")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateToken(UserLoginVM model)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: false, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
                return NotFound();

            var user = await _userManager.FindByNameAsync(model.UserName);
            user.LastLoginDateTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            UserAuthenticateVM returnVM = new UserAuthenticateVM();
            returnVM = _mapper.Map<User, UserAuthenticateVM>(user);
            returnVM.Token = GetToken(user);

            return Created("", returnVM);
        }

        [HttpGet("RefreshToken")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken()
        {
            var user = await _userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(UserAddVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User entity = _mapper.Map<UserAddVM, User>(model);
                    entity.Id = Guid.NewGuid();
                    entity.CreateDateTime = DateTime.UtcNow;
                    entity.LastLoginDateTime = DateTime.UtcNow;
                    var identityResult = await _userManager.CreateAsync(entity, model.PasswordHash);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(entity, isPersistent: false);


                        UserAuthenticateVM returnVM = new UserAuthenticateVM();
                        returnVM = _mapper.Map<User, UserAuthenticateVM>(entity);
                        returnVM.Token = GetToken(entity);

                        return Created("", returnVM);
                    }
                    else
                    {
                        return Conflict();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }

        [HttpGet("GetToken")]
        private String GetToken(User user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("Jwt:Key")));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_config.GetValue<int>("Jwt:ExpiryDuration")),
                audience: _config.GetValue<String>("Jwt:Audience"),
                issuer: _config.GetValue<String>("Jwt:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
