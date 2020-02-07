using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGA.Core.Validation;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;

namespace NGA.MonolithAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    public class GroupController : DefaultApiCRUDController<GroupAddVM, GroupUpdateVM, GroupVM, Group, IGroupService>
    {
        public GroupController(IGroupService service, ILogger<GroupController> logger)
             : base(service, logger)
        {

        }

        [HttpGet("GetByUserId/{userId}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual ActionResult GetByUserId(Guid userId)
        {
            try
            {
                if (userId.IsNull())
                    return BadRequest();

                var result = _service.GetByUserId(userId);

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

        [HttpGet("{groupId}/users")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual ActionResult GetUsers(Guid groupId)
        {
            try
            {
                if (groupId.IsNull())
                    return BadRequest();

                var result = _service.GetUsers(groupId);

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
    }
}