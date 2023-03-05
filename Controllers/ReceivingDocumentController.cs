using CarpenterAPI.Data;
using CarpenterAPI.Models.Component;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models.Receiving;
using CarpenterAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarpenterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReceivingDocumentController : ControllerBase
    {
        private readonly ReceivingDocumentRepository repository;

        public ReceivingDocumentController(APIDBContext dbContext)
        {
            repository = new ReceivingDocumentRepository(dbContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.Get(null, null, "Lines,Lines.Product", null));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetSingle(int id)
        {
            var document = repository.GetByID(id);
            if(document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddReceivingDocumentRequest addRequest)
        {
            var lines = repository.CreateLines(addRequest.ProductQuantities);

            var receivingDocument = new ReceivingDocument
            {
                CreatedDate = DateTime.Now,
                Status = ReceivingDocumentStatus.New,
                Lines = lines.ToArray()
            };

            repository.InsertLines(lines);
            repository.Insert(receivingDocument);
            repository.Save();
            return Ok(receivingDocument);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateReceivingDocumentRequest request)
        {
            var document = repository.GetByID(id);
            if (document == null) 
            { 
                return NotFound(); 
            }

            repository.RemoveLines(document);
            document.Lines = repository.CreateLines(request.ProductQuantities);
            repository.InsertLines(document.Lines.ToArray());
            repository.Save();
            return Ok(document);
        }

        // DELETE api/<ReceivingDocumentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)  
        {
        }
    }
}
