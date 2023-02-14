using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarpenterAPI.Models.Product;

namespace CarpenterAPI.Models.Component
{
    public class ProductComponent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        public Product.Product Product { get; set; }

        [Required]
        public Product.Product ComponentProduct { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool Required { get; set; }
    }
}
