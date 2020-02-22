using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGA.Core.Validation;
using NGA.Data.MongoDB;
using NGA.Domain;

namespace NGA.MonolithAPI.Controllers.V2
{
    public class PictureController : DefaultApiController
    {
        private readonly IMongoService mongoService;

        public PictureController(IMongoService service,
            ILogger<UserController> logger)
             : base(logger)
        {
            this.mongoService = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual ActionResult Get(string id)
        {
            try
            {
                if (Validation.IsNullOrEmpty(id))
                    return BadRequest();

                var result = mongoService.Get(id);

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

        [HttpPost]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual ActionResult Add([FromBody]Picture model)
        {
            try
            {
                if (Validation.IsNull(model))
                    return BadRequest();

                var result = mongoService.Create(model);

                if (result.IsNull())
                    return NotFound();

                return Created("", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }
    }
}