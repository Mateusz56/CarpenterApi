using System.ComponentModel.DataAnnotations;

namespace CarpenterAPI.Models.Component
{
    public class ProductComponentDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ComponentName { get; set; }
        public string ComponentDescription { get; set; }
        public int Quantity { get; set; }
        public bool Required { get; set; }
    }
}
