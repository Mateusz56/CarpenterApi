using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Workstation
{
    public class AddWorkstationRequest
    {
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, MinLength(3)]
        public string Description { get; set; }

        [Required]
        public WorkstationType Type { get; set; }

        [Required]
        public int Icon { get; set; }

        [Required]
        public int Color { get; set; }

        [Required]
        public bool AllowMultipleOperations { get; set; }
    }
}
