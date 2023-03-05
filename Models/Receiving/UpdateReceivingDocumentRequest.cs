using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Receiving
{
    public class UpdateReceivingDocumentRequest
    {
        [Required, MinLength(1)]
        public ProductQuantity[] ProductQuantities { get; set; }
    }
}
