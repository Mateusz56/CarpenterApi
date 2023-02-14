using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Component
{
    public class LinkComponentRequest
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        public int ComponentProductID { get; set; }

        [ComponentQuantityCheck(ErrorMessage = "Quantity is required for required components."), Range(0, int.MaxValue, ErrorMessage = "Quantity must be higher than zero.")]
        public int Quantity { get; set; }
        public bool Required { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ComponentQuantityCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;
            string[] memberNames = new string[] { validationContext.MemberName };

            LinkComponentRequest linkComponentRequest = (LinkComponentRequest)validationContext.ObjectInstance;

            if (linkComponentRequest.Required && linkComponentRequest.Quantity == 0)
                result = new ValidationResult(string.Format(this.ErrorMessage, 500), memberNames);

            return result;
        }
    }
}
