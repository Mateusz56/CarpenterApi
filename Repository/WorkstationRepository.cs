using CarpenterAPI.Data;
using CarpenterAPI.Models.Workstation;

namespace CarpenterAPI.Repository
{
    public class WorkstationRepository : GenericRepository<Workstation>
    {
        public WorkstationRepository(APIDBContext dbContext) : base(dbContext)
        {
        }


    }
}
