using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CarpenterAPI.Models.Product
{
    public enum ProductType
    {
        Product,
        Component,
        Scrap
    }

    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(50), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [StringLength(200), Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public ProductType ProductType { get; set; }
    }
}
