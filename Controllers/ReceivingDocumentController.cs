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

            if (document.Status != ReceivingDocumentStatus.New && document.Status != ReceivingDocumentStatus.Modified && document.Status != ReceivingDocumentStatus.Rejected)
                return Forbid();

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
                return Forbid();

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
                return Forbid();

            document.Status = ReceivingDocumentStatus.Rejected;

            repository.Save();
            return Ok(document);
        }

        [HttpPatch("archive/{id:int}")]
        public IActionResult Archive(int id)
        {
            var document = repository.GetByID(id);

            if (document.Status != ReceivingDocumentStatus.New && document.Status != ReceivingDocumentStatus.Modified && document.Status != ReceivingDocumentStatus.Rejected)
                return Forbid();

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
