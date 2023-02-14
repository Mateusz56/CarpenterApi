using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Product
{
    public class UpdateProductRequest
    {
        [Required(AllowEmptyStrings = false), MinLength(3, ErrorMessage = "The field Name must be a string with a minimum length of 3.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        
        public ProductType ProductType { get; set; }
    }
}
