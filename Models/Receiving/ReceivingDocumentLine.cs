using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarpenterAPI.Models.Product;

namespace CarpenterAPI.Models.Receiving
{
    public class ProductQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ReceivingDocumentLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        public Product.Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
