using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductTypeName { get; set; }
    }
}
