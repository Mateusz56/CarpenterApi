namespace CarpenterAPI.Models.Workstation
{
    public class WorkstationFilters
    {
        public string? NameLike { get; set; }
        public string? DescriptionLike { get; set; }
        public bool? AllowMultipleOperations { get; set; }
        public WorkstationType[]? WorkstationTypes { get; set; }
        public WorkstationStatus[]? WorkstationStatuses { get; set; }
        public int[]? WorkstationColors { get; set; }
        public int[]? WorkstationIcons { get; set; }
    }
}
