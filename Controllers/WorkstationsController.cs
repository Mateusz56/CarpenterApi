using CarpenterAPI.Data;
using CarpenterAPI.Models;
using CarpenterAPI.Models.Workstation;
using CarpenterAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarpenterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkstationsController : ControllerBase
    {
        private readonly WorkstationRepository repository;

        public WorkstationsController(APIDBContext dbContext)
        {
            repository = new WorkstationRepository(dbContext);
        }

        // GET: api/<WorkstationsController>
        [HttpGet]
        public IActionResult Get([FromQuery] WorkstationFilters filters, [FromQuery] Paging page)
        {
            var workstations = repository.GetWithCount(
                out int count,
                filter: repository.CreateFiltersFunctionsArray(filters),
                orderBy: x => x.Name,
                page: page,
                orderByDescending: true
                );

            return Ok(new { count, values = workstations});
        }

        // GET api/<WorkstationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WorkstationsController>
        [HttpPost]
        public IActionResult Post([FromBody] AddWorkstationRequest request)
        {
            var workstation = new Workstation()
            {
                Name = request.Name,
                Description = request.Description,
                Status = WorkstationStatus.Active,
                Type = request.Type,
                AllowMultipleOperations = request.AllowMultipleOperations,
                Icon = request.Icon,
                Color = request.Color
            };

            repository.Insert(workstation);
            repository.Save();
            return Ok(workstation);
        }

        // PUT api/<WorkstationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorkstationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
