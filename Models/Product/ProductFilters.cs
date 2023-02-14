namespace CarpenterAPI.Models.Product
{
    public class ProductFilters
    {
        public string? NameLike { get; set; }
        public string? DescriptionLike { get; set; }
        public int[]? ProductTypeList { get; set; }
    }
}
