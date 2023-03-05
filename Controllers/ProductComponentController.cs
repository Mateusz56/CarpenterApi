using CarpenterAPI.Data;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CarpenterAPI.Models.Component;
using Microsoft.EntityFrameworkCore;
using CarpenterAPI.Repository;
using System.Linq.Expressions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarpenterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductComponentController : Controller
    {
        private readonly ProductComponentRepository repository;

        public ProductComponentController(APIDBContext dbContext)
        {
            repository = new ProductComponentRepository(dbContext);
        }

        [HttpGet]
        public IActionResult GetProductComponents(int productID)
        {
            return Ok(repository.Get(
                filter: new Expression<Func<ProductComponent, bool>>[] { x => x.Product.Id == productID },
                includeProperties: "Product,ComponentProduct"
                )
                .Select(repository.ConvertToProductComponentDTO));
        }

        [HttpPost]
        public IActionResult LinkComponent(LinkComponentRequest linkComponentRequest)
        {
            var productComponent = repository.CreateProductComponent(linkComponentRequest);

            repository.Insert(productComponent);
            repository.Save();

            return Ok(productComponent);
        }

        [HttpPut]
        public IActionResult UpdateComponents(UpdateProductComponentRequest[] updateComponentsRequest)
        {
            foreach(var updateRequest in updateComponentsRequest)
            {
                var component = repository.GetByID(updateRequest.Id);

                component.Required = updateRequest.Required != null ? updateRequest.Required.Value : component.Required;
                component.Quantity = updateRequest.Quantity != null ? updateRequest.Quantity.Value : component.Quantity;
            }

            repository.Save();
            return Ok(updateComponentsRequest);
        }

        [HttpDelete]
        public IActionResult DeleteComponent(int[] id)
        {
            var components = repository.Delete(id);
            repository.Save();

            return Ok(components);
        }
    }
}
