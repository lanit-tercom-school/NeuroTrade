using System.Security.AccessControl;
using NeuroTradeAPI;

namespace Repository
{
    public class CandleRepository : GenericRepository<Candle>
    {
        public CandleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}