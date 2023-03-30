using CarpenterAPI.Data;
using CarpenterAPI.Models.Product;
using CarpenterAPI.Models.Workstation;
using System.Linq.Expressions;

namespace CarpenterAPI.Repository
{
    public class WorkstationRepository : GenericRepository<Workstation>
    {
        public WorkstationRepository(APIDBContext dbContext) : base(dbContext)
        {
        }

        public Expression<Func<Workstation, bool>>[] CreateFiltersFunctionsArray(WorkstationFilters filters)
        {
            if (filters != null)
            {
                var functions = new List<Expression<Func<Workstation, bool>>>();
                if (filters.WorkstationTypes != null && filters.WorkstationTypes.Length > 0)
                    functions.Add(x => filters.WorkstationTypes.Contains(x.Type));
                if (filters.WorkstationStatuses != null && filters.WorkstationStatuses.Length > 0)
                    functions.Add(x => filters.WorkstationStatuses.Contains(x.Status));
                if (filters.WorkstationColors != null && filters.WorkstationColors.Length > 0)
                    functions.Add(x => filters.WorkstationColors.Contains(x.Color));
                if (filters.WorkstationIcons != null && filters.WorkstationIcons.Length > 0)
                    functions.Add(x => filters.WorkstationIcons.Contains(x.Icon));
                if (filters.NameLike != null)
                    functions.Add(x => x.Name.ToLower().Contains(filters.NameLike.ToLower()));
                if (filters.DescriptionLike != null)
                    functions.Add(x => x.Description.ToLower().Contains(filters.DescriptionLike.ToLower()));
                if (filters.AllowMultipleOperations != null)
                    functions.Add(x => x.AllowMultipleOperations == filters.AllowMultipleOperations);

                return functions.ToArray();
            }

            return null;
        }
    }
}
