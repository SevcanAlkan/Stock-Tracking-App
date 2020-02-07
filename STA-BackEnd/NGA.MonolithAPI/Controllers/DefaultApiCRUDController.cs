using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGA.Core.EntityFramework;
using NGA.Core.Model;
using NGA.Core.Validation;
using NGA.Data.SubStructure;
using System;
using System.Threading.Tasks;

namespace NGA.MonolithAPI.Controllers.V2
{
    public abstract class DefaultApiCRUDController<A, U, G, D, S> : DefaultApiController
             where A : AddVM, IAddVM, new()
             where U : UpdateVM, IUpdateVM, new()
             where G : BaseVM, IBaseVM, new()
             where D : Base, IBase, new()
             where S : IBaseService<A, U, G, D>
    {
        protected S _service;

        public DefaultApiCRUDController(S service, ILogger<DefaultApiCRUDController<A, U, G, D, S>> logger)
            : base(logger)
        {
            this._service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual ActionResult Get()
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
        public virtual async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                if (Validation.IsNullOrEmpty(id))
                    return BadRequest();

                var result = await _service.GetById(id);

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
        public virtual async Task<ActionResult> Add(A model)
        {
            try
            {
                if (Validation.IsNull(model))
                    return BadRequest();

                var result = await _service.Add(model);

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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Update(Guid id, U model)
        {
            try
            {
                if (Validation.IsNull(model))
                    return BadRequest();

                var result = await _service.Update(id, model);

                if (result.IsNull())
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {           
            try
            {
                if (id == null || id == Guid.Empty)
                    return BadRequest();

                var result = await _service.Delete(id);

                if (result.IsNull())
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem(ex.ToString());
            }
        }
    }

    public interface IDefaultApiCRUDController<A, U, G, D, S>
            where A : AddVM, IAddVM, new()
            where U : UpdateVM, IUpdateVM, new()
            where G : BaseVM, IBaseVM, new()
            where D : Base, IBase, new()
            where S : IBaseService<A, U, G, D>
    {
        JsonResult Get();
        Task<ActionResult> GetById(Guid id);
        Task<ActionResult> Add(A model);
        Task<ActionResult> Update(Guid id, U model);
        Task<ActionResult> Delete(Guid id);
    }
}