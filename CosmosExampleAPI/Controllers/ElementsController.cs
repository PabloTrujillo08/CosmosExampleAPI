using CosmosExampleAPI.Interface;
using CosmosExampleAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmosExampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //cors
    [EnableCors("MyPolicy")]
    public class ElementsController : ControllerBase
    {
        private readonly ICosmosDbService _iCosmosDbService;

        public ElementsController(ICosmosDbService iCosmosDbService)
        {
            _iCosmosDbService = iCosmosDbService;
        }

        // GET: api/Elements
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _iCosmosDbService.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<Elements> Get(string id)
        {
            return await _iCosmosDbService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Elements element)
        {
            element.Id = Guid.NewGuid().ToString();
            await _iCosmosDbService.AddAsync(element);
            return CreatedAtAction("Get", new { id = element.Id }, element);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Elements element)
        {
            await _iCosmosDbService.UpdateAsync(id, element);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _iCosmosDbService.DeleteAsync(id);
            return NoContent();
        }

        


    }
}
