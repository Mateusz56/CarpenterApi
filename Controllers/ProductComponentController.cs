using CarpenterAPI.Data;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CarpenterAPI.Models.Component;
using Microsoft.EntityFrameworkCore;

namespace CarpenterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductComponentController : Controller
    {
        private readonly APIDBContext dbContext;

        public ProductComponentController(APIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private ProductComponentDTO ConvertToProductComponentDTO(ProductComponent component)
        {
            return new ProductComponentDTO
            {
                Id = component.Id,
                ProductName = component.Product.Name,
                ComponentName = component.ComponentProduct.Name,
                ComponentDescription = component.ComponentProduct.Description,
                Quantity = component.Quantity,
                Required = component.Required
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetProductComponents(int productID)
        {
            return Ok(dbContext.ProductComponents
                .Where(x => x.Product.Id == productID)
                .Include(x => x.Product)
                .Include(x => x.ComponentProduct)
                .Select(ConvertToProductComponentDTO));
        }

        [HttpPost]
        public async Task<IActionResult> LinkComponent(LinkComponentRequest linkComponentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await dbContext.Products.FindAsync(linkComponentRequest.ProductID);
            var component = await dbContext.Products.FindAsync(linkComponentRequest.ComponentProductID);

            if(component == null || product == null) 
            {
                return NotFound();
            }

            var productComponent = new ProductComponent()
            {
                Product = product,
                ComponentProduct = component,
                Quantity = linkComponentRequest.Quantity,
                Required = linkComponentRequest.Required
            };

            await dbContext.ProductComponents.AddAsync(productComponent);
            await dbContext.SaveChangesAsync();

            return Ok(productComponent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComponents(UpdateProductComponentRequest[] updateComponentsRequest)
        {
            foreach(var updateRequest in updateComponentsRequest)
            {
                var component = await dbContext.ProductComponents.FindAsync(updateRequest.Id);
                if (component == null)
                    return NotFound();

                component.Required = updateRequest.Required != null ? updateRequest.Required.Value : component.Required;
                component.Quantity = updateRequest.Quantity != null ? updateRequest.Quantity.Value : component.Quantity;
            }

            await dbContext.SaveChangesAsync();
            return Ok(updateComponentsRequest);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComponent(int[] id)
        {
            var components = dbContext.ProductComponents.Where(x => id.Contains(x.Id));

            if (components.Count() == 0)
                return NotFound();

            dbContext.ProductComponents.RemoveRange(components);

            await dbContext.SaveChangesAsync();

            return Ok(components);
        }
    }
}
