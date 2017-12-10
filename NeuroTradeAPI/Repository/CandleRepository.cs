using System.Security.AccessControl;
using NeuroTradeAPI;
using NeuroTradeAPI.Entities;

namespace Repository
{
    public class CandleRepository : GenericRepository<Candle>
    {
        public CandleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}