using CarpenterAPI.Data;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models.Receiving;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public Expression<Func<ReceivingDocument, bool>>[] CreateFiltersFunctionsArray(ReceivingDocumentFilters filters)
        {
            if (filters != null)
            {
                var functions = new List<Expression<Func<ReceivingDocument, bool>>>();
                if (filters.IdMin != null)
                    functions.Add(x => x.Id >= filters.IdMin);
                if (filters.IdMax != null)
                    functions.Add(x => x.Id <= filters.IdMax);
                if (filters.StatusList != null && filters.StatusList.Length > 0)
                    functions.Add(x => filters.StatusList.Contains((int)x.Status));
                if (filters.ValidatedBefore != null)
                    functions.Add(x => x.ValidationDate <= filters.ValidatedBefore);
                if (filters.ValidatedAfter != null)
                    functions.Add(x => x.ValidationDate >= filters.ValidatedAfter);
                if (filters.CreatedBefore != null)
                    functions.Add(x => x.CreatedDate <= filters.CreatedBefore);
                if (filters.CreatedAfter != null)
                    functions.Add(x => x.CreatedDate >= filters.CreatedAfter);

                return functions.ToArray();
            }

            return null;
        }
    }
}
