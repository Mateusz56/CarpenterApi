using CarpenterAPI.Data;
using CarpenterAPI.Models.Receiving;
using Microsoft.EntityFrameworkCore;

namespace CarpenterAPI.Repository
{
    public class ReceivingDocumentRepository : GenericRepository<ReceivingDocument>
    {
        public ReceivingDocumentRepository(APIDBContext dbContext) : base(dbContext)
        {
        }

        public ReceivingDocumentLine[] CreateLines(ProductQuantity[] productQuantities)
        {
            var lines = new List<ReceivingDocumentLine>();
            foreach (var productQuantity in productQuantities)
            {
                var product = dbContext.Products.Find(productQuantity.ProductId);

                lines.Add(new ReceivingDocumentLine
                {
                    Product = product,
                    Quantity = productQuantity.Quantity
                });
            }

            return lines.ToArray();
        }

        public void InsertLines(ReceivingDocumentLine[] lines)
        {
            dbContext.ReceivingDocumentLines.AddRange(lines);
        }

        public void RemoveLines(ReceivingDocument document)
        {
            var lines = document.Lines;
            document.Lines.Clear();
            dbContext.ReceivingDocumentLines.RemoveRange(lines);
        }

        public ReceivingDocument GetByID(int id)
        {
            return dbSet
                .Include("Lines")
                .Include("Lines.Product")
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
