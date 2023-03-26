using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpenterAPI.Models.Workstation
{
    public enum WorkstationType
    {
        Saw,
        Painting,
        Oiling,
        Drafting,
        Inspection
    }

    public enum WorkstationStatus
    {
        New,
        Active,
        Maintenance,
        Archived
    }

    public class Workstation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]

        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, MinLength(3)]
        public string Description { get; set; }

        [Required]
        public WorkstationStatus Status { get; set; }

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
