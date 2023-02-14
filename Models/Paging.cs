namespace CarpenterAPI.Models
{
    public class Paging
    {
        public static readonly int DefaultPageSize = 30;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
