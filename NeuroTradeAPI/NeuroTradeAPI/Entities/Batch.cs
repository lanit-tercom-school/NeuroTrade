using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI
{
    public class Batch
    {
        public int BatchId { get; set; }
        public int InstrumentId { get; set; }
        public TimeSpan Interval { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }
        
        public Dictionary<string, object> toDict()
        {
            return new Dictionary<string, object>()
            {
                {"id", BatchId},
                {"Start", BeginTime.ToString()},
                {"Interval", Interval}
            };
        }
    }
}