using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Component
{
    public class UpdateProductComponentRequest
    {
        [Required]
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public bool? Required { get; set; }
    }
}
