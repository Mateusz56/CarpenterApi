using Microsoft.EntityFrameworkCore;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models.Component;
using CarpenterAPI.Models.Receiving;

namespace CarpenterAPI.Data
{
    public class APIDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<ReceivingDocument> ReceivingDocuments { get; set; }
        public DbSet<ReceivingDocumentLine> ReceivingDocumentLines { get; set; }

        public APIDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
