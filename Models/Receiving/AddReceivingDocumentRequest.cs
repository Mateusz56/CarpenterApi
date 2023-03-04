using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Receiving
{
    public class AddReceivingDocumentRequest
    {
        public class ProductQuantity
        {
            public int ProductId { get; set;}
            public int Quantity { get; set;}
        }

        [Required, MinLength(1)]
        public ProductQuantity[] ProductQuantities { get; set;}
    }
}
