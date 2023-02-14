using Microsoft.EntityFrameworkCore;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models.Component;

namespace CarpenterAPI.Data
{
    public class APIDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        
        public APIDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
