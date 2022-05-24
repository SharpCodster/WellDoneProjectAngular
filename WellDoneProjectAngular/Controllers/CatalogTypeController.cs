using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WellDoneProjectAngular.Core.Dtos;
using WellDoneProjectAngular.Core.Interfaces.Services;

namespace WellDoneProjectAngular.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CatalogTypeController : ControllerBase
    { /*
            GET /tickets - Retrieves a list of tickets
            GET /tickets/12 - Retrieves a specific ticket
            POST /tickets - Creates a new ticket
            PUT /tickets/12 - Updates ticket #12
            PATCH /tickets/12 - Partially updates ticket #12
            DELETE /tickets/12 - Deletes ticket #12
        */

        private readonly ILogger _logger;
        private ICatalogTypeService _service;

        public CatalogTypeController(ILoggerFactory loggerFactory, ICatalogTypeService service)
        {
            _logger = loggerFactory.CreateLogger("CatalogTypeController");
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            return Ok(await _service.ListAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await _service.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] CatalogTypeDto newAccount)
        {
            var t = await _service.CreateAsync(newAccount);
            Uri uri = new Uri($"Api/CatalogType/{t.Id}", UriKind.Relative);
            return Created(uri, t);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<IActionResult> Update([FromBody] CatalogTypeDto newAccount)
        {
            await _service.UpdateAsync(newAccount);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        public virtual async Task<IActionResult> Patch(int id, [FromBody] Dictionary<string, object> valuesToPatch)
        {
            await _service.PatchAsync(id, valuesToPatch);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
