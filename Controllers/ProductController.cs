using CarpenterAPI.Data;
using CarpenterAPI.Models;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarpenterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductRepository repository;

        public ProductController(APIDBContext dbContext)
        {
            repository = new ProductRepository(dbContext);
        }

        [HttpGet]
        public IActionResult GetProduct([FromQuery] Paging page,[FromQuery] ProductFilters filters)
        {
            var products = repository.Get(
                filter: repository.CreateFiltersFunctionsArray(filters), 
                page: page, 
                orderBy: x => x.Name);

            int count = products.Count();

            return Ok(new { values = products.Select(repository.ConvertToProductDTO), count });
        }

        [HttpGet("types")]
        public IActionResult GetProductTypes()
        {
            var productTypes = new Dictionary<int, string>();
            var enumValues = (int[])Enum.GetValues(typeof(ProductType));
            var enumNames = (string[])Enum.GetNames(typeof(ProductType));

            for(int i = 0; i < enumValues.Length; i++)
            {
                productTypes.Add(enumValues[i], enumNames[i]);
            }
            return Ok(productTypes);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            var product = repository.GetByID(id);
            
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductRequest addProductRequest)
        {
            var product = new Product()
            {
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                ProductType = addProductRequest.ProductType
            };

            repository.Insert(product);
            repository.Save();

            return Ok(product);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateProduct([FromRoute] int id, UpdateProductRequest updateProductRequest)
        {
            var product = repository.GetByID(id);

            if (product == null)
                return NotFound();

            product.Description = updateProductRequest.Description;
            product.Name = updateProductRequest.Name;
            product.ProductType = updateProductRequest.ProductType;

            repository.Save();

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var product = repository.GetByID(id);

            if(product == null)
                return NotFound();

            repository.Delete(id);
            repository.Save();

            return Ok(product);
        }
    }
}
