using CarpenterAPI.Data;
using CarpenterAPI.Models.Component;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models.Receiving;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarpenterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReceivingDocumentController : ControllerBase
    {
        private readonly APIDBContext dbContext;

        public ReceivingDocumentController(APIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<ReceivingDocumentController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(dbContext.ReceivingDocuments.Include(x => x.Lines).Include("Lines.Product"));
        }

        // POST api/<ReceivingDocumentController>
        [HttpPost]
        public IActionResult Post([FromBody] AddReceivingDocumentRequest addRequest)
        {
            var lines = new List<ReceivingDocumentLine>();
            foreach(var productQuantity in addRequest.ProductQuantities)
            {
                var product = dbContext.Products.Find(productQuantity.ProductId);
                if (product == null)
                    return NotFound();

                lines.Add(new ReceivingDocumentLine
                {
                    Product = product,
                    Quantity = productQuantity.Quantity
                });
            }

            var receivingDocument = new ReceivingDocument
            {
                CreatedDate = DateTime.Now,
                Status = ReceivingDocumentStatus.New,
                Lines = lines.ToArray()
            };

            dbContext.AddRange(lines);
            dbContext.Add(receivingDocument);
            dbContext.SaveChanges();
            return Ok(receivingDocument);
        }

        // PUT api/<ReceivingDocumentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReceivingDocumentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
