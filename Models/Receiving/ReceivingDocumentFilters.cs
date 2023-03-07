namespace CarpenterAPI.Models.Receiving
{
    public class ReceivingDocumentFilters
    {
        public int? IdMin { get; set; }
        public int? IdMax { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? CreatedAfter { get; set;}
        public DateTime? ValidatedBefore { get; set; }
        public DateTime? ValidatedAfter { get; set; }
        public int[]? StatusList { get; set; }
    }
}
