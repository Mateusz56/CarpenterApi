using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Receiving
{
    public class AddReceivingDocumentRequest : IValidatableObject
    {
        [Required, MinLength(1, ErrorMessage = "Receiving document can not be empty.")]
        public ProductQuantity[] ProductQuantities { get; set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            for (int i = 0; i < ProductQuantities.Length; i++)
            {
                if (ProductQuantities[i].ProductId == 0 || ProductQuantities[i].Quantity <= 0) {
                    yield return new ValidationResult("Product and quantity must be provided.", new[] { nameof(ProductQuantities) });
                    break;
                }
            }
        }
    }
}
