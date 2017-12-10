using NeuroTradeAPI;
using NeuroTradeAPI.Entities;

namespace Repository
{
    public class BatchRepository : GenericRepository<Batch>
    {
        public BatchRepository(ApplicationContext context) : base(context)
        {
        }
    }
}