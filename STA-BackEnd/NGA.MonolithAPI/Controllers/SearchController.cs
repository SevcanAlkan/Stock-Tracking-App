using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGA.Data.Service;
using System.Linq;

namespace NGA.MonolithAPI.Controllers.V2
{
    public class SearchController : DefaultApiController
    {
        private IMessageService _service;

        public SearchController(IMessageService service, ILogger<SearchController> logger)
             : base(logger)
        {
            this._service = service;
        }

        [HttpGet("{key}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public ActionResult Get(string key)
        {
            if (key == null || key == "" || key.Length < 4)
            {
                return BadRequest();
            }

            var messages = _service.Repository.Query().Where(s => s.Text.Contains(key)).ToList();

            return Ok(messages);
        }
    }
}