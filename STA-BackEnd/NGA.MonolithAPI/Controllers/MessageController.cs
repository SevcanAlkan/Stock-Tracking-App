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
    public class MessageController : DefaultApiCRUDController<MessageAddVM, MessageUpdateVM, MessageVM, Message, IMessageService>
    {
        public MessageController(IMessageService service, ILogger<MessageController> logger)
             : base(service, logger)
        {

        }

        [HttpGet("GetByGroupId/{groupId}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual ActionResult GetByGroupId(Guid groupId)
        {
            try
            {
                if (groupId.IsNull())
                    return BadRequest();

                var result = _service.GetMessagesByGroupId(groupId);

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