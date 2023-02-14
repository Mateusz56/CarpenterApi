using CarpenterAPI.Data;
using CarpenterAPI.Models;
using CarpenterAPI.Models.Product;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarpenterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly APIDBContext dbContext;

        public ProductController(APIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private ProductDTO ConvertToProductDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ProductType = product.ProductType,
                ProductTypeName = Enum.GetName(typeof(ProductType), product.ProductType)
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] Paging page,[FromQuery] ProductFilters filters)
        {
            IQueryable<Product> products = dbContext.Products.OrderBy(x => x.Name);
            
            if(filters != null)
            {
                if (filters.ProductTypeList != null && filters.ProductTypeList.Length > 0)
                    products = products.Where(x => filters.ProductTypeList.Contains((int)x.ProductType));
                if(filters.NameLike != null)
                    products = products.Where(x => x.Name.ToLower().Contains(filters.NameLike.ToLower()));
                if(filters.DescriptionLike != null)
                    products = products.Where(x => x.Description.ToLower().Contains(filters.DescriptionLike.ToLower()));
            }
            int count = products.Count();

            if(page.PageIndex != 0)
                products = products.Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);

            return Ok(new { values = products.Select(ConvertToProductDTO), count });
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
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

        [HttpGet("id:int")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetProducts(string name)
        {
            var products = dbContext.Products.Where(x => x.Name.Equals(name)).ToList();

            if(products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product()
            {
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                ProductType = addProductRequest.ProductType
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, UpdateProductRequest updateProductRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await dbContext.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            product.Description = updateProductRequest.Description;
            product.Name = updateProductRequest.Name;
            product.ProductType = updateProductRequest.ProductType;

            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product == null)
                return NotFound();

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
