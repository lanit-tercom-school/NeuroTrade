using NeuroTradeAPI;

namespace Repository
{
    public class BatchRepository : GenericRepository<Batch>
    {
        public BatchRepository(ApplicationContext context) : base(context)
        {
        }
    }
}