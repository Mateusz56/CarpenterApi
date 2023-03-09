using CarpenterAPI.Data;
using CarpenterAPI.Models.Product;
using System.Linq.Expressions;

namespace CarpenterAPI.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(APIDBContext dbContext) : base(dbContext)
        {
        }

        public Expression<Func<Product, bool>>[] CreateFiltersFunctionsArray(ProductFilters filters)
        {
            if (filters != null)
            {
                var functions = new List<Expression<Func<Product, bool>>>();
                if (filters.ProductTypeList != null && filters.ProductTypeList.Length > 0)
                    functions.Add(x => filters.ProductTypeList.Contains((int)x.ProductType));
                if (filters.NameLike != null)
                    functions.Add(x => x.Name.ToLower().Contains(filters.NameLike.ToLower()));
                if (filters.DescriptionLike != null)
                    functions.Add(x => x.Description.ToLower().Contains(filters.DescriptionLike.ToLower()));

                return functions.ToArray();
            }

            return null;
        }

        public ProductDTO ConvertToProductDTO(Product product)
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

        public string CheckIfCanDelete(Product product)
        {
            return dbContext.ReceivingDocumentLines.Any(x => x.Product == product) ? "Can't remove this product. It is referenced by receiving document." : null;
        }
    }
}
