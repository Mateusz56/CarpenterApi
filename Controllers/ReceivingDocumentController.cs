using CarpenterAPI.Data;
using CarpenterAPI.Models;
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
        public IActionResult Get([FromQuery] ReceivingDocumentFilters filters, [FromQuery] Paging paging)
        {
            var documents = repository.GetWithCount(out int count, repository.CreateFiltersFunctionsArray(filters), (x) => x.CreatedDate, "Lines,Lines.Product", paging, true);
 
            return Ok(new {values = documents, count});
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

            if (document.Status != ReceivingDocumentStatus.New && document.Status != ReceivingDocumentStatus.Modified && document.Status != ReceivingDocumentStatus.Rejected)
                return Conflict();

            repository.RemoveLines(document);
            
            var lines = repository.CreateLines(request.ProductQuantities);
            
            document.Lines = lines.ToList();
            repository.InsertLines(lines);

            document.Status = ReceivingDocumentStatus.Modified;

            repository.Save();
            return Ok(document);
        }

        [HttpPost("{id:int}")]
        public IActionResult Validate(int id)
        {
            var document = repository.GetByID(id);

            if (document.Status != ReceivingDocumentStatus.New && document.Status != ReceivingDocumentStatus.Modified)
                return Conflict();

            document.Status = ReceivingDocumentStatus.Accepted;
            document.ValidationDate = DateTime.Now;

            // increase inventory
            repository.Save();
            return Ok(document);
        }

        [HttpPatch("{id:int}")]
        public IActionResult Reject(int id)
        {
            var document = repository.GetByID(id);

            if (document.Status != ReceivingDocumentStatus.New && document.Status != ReceivingDocumentStatus.Modified)
                return Conflict();

            document.Status = ReceivingDocumentStatus.Rejected;

            repository.Save();
            return Ok(document);
        }

        [HttpPatch("archive/{id:int}")]
        public IActionResult Archive(int id)
        {
            var document = repository.GetByID(id);

            if (document.Status != ReceivingDocumentStatus.New && document.Status != ReceivingDocumentStatus.Modified && document.Status != ReceivingDocumentStatus.Rejected)
                return Conflict();

            document.Status = ReceivingDocumentStatus.Archived;

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
