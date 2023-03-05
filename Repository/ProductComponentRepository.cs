using CarpenterAPI.Data;
using CarpenterAPI.Models.Component;
using Microsoft.OpenApi.Validations;

namespace CarpenterAPI.Repository
{
    public class ProductComponentRepository : GenericRepository<ProductComponent>
    {
        public ProductComponentRepository(APIDBContext dbContext) : base(dbContext)
        {
        }

        public ProductComponentDTO ConvertToProductComponentDTO(ProductComponent component)
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

        public ProductComponent CreateProductComponent(LinkComponentRequest request)
        {
            var product = dbContext.Products.Find(request.ProductID);
            var component = dbContext.Products.Find(request.ComponentProductID);

            return new ProductComponent()
            {
                Product = product,
                ComponentProduct = component,
                Quantity = request.Quantity,
                Required = request.Required
            };
        }

        public ProductComponent[] Delete(int[] ids)
        {
            ProductComponent[] components = dbSet.Where(x => ids.Contains(x.Id)).ToArray();
            dbSet.RemoveRange(components);
            return components;
        }
    }
}
