using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI
{
    public class Candle
    {
        public long CandleId { get; set; }
        public int BatchId { get; set; }
        public float Open { get; set; }
        public float Close { get; set; }
        public float Low { get; set; }
        public float High { get; set; }
        public int Volume { get; set; }
        public DateTime? BeginTime { get; set; }
        [ForeignKey("BatchId")]
        public Batch Batch { get; set; }
        
        public Dictionary<string, object> toDict()
        {
            return new Dictionary<string, object>()
            {
                {"id", CandleId},
                {"start", BeginTime},
                {"open", Open},
                {"close", Close},
                {"low", Low},
                {"high", High},
                {"volume", Volume}
            };
        }
    }
}