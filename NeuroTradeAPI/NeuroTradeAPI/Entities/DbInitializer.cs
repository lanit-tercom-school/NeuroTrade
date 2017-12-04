using System;
using System.Linq;

namespace NeuroTradeAPI
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Batches.Any())
                return;

            var batches = new Batch[]
            {
                // Здесь со временем всё хорошо, поэтому просто Parse. Но вообще используйте tryParse
                new Batch{BatchId=1,Alias="RTS-12.17(RIZ7)",Interval=TimeSpan.Parse("1:00:00"),Timestamp=DateTime.Parse("01/11/2017 10:00:00")},
                new Batch{BatchId=2,Alias="SPFB.SBPR-12.17",Interval=TimeSpan.Parse("0:30:00"),Timestamp=DateTime.Parse("01/11/2017 10:00:00")}
            };
            foreach (Batch b in batches)
                context.Batches.Add(b);
            context.SaveChanges();

            var candles = new Candle[]
            {
                new Candle{BatchId=1,Open=111700,High=112600,Low=111700,Close=112480,Volume=80244},
                new Candle{BatchId=1,Open=112470,High=112720,Low=112470,Close=112510,Volume=38690},
                new Candle{BatchId=2,Open=16047,High=16115,Low=16001,Close=16091,Volume=1751},
                new Candle{BatchId=2,Open=16091,High=16092,Low=16058,Close=16086,Volume=919}
            };
            foreach (Candle c in candles)
            {
                context.Candles.Add(c);
            }
            context.SaveChanges();
        }
    }
}