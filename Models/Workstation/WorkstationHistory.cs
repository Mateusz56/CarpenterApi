using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Workstation
{
    public class WorkstationHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        public Workstation Workstation { get; set; }

        [Required]
        public WorkstationStatus Status { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
    }
}
