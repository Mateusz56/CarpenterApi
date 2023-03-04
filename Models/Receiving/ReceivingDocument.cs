using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Receiving
{
    public enum ReceivingDocumentStatus
    {
        New,
        Accepted,
        Rejected,
        Modified,
        Archived
    }

    public class ReceivingDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        public ReceivingDocumentStatus Status { get; set;}

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ValidationDate { get; set;}

        public ICollection<ReceivingDocumentLine> Lines { get; set;}
    }
}
