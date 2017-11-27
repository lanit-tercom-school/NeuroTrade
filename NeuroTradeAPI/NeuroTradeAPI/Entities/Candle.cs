using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI
{
    public class Candle
    {
        public int CandleId { get; set; }
        public int BatchId { get; set; }
        public float Open { get; set; }
        public float Close { get; set; }
        public float Low { get; set; }
        public float High { get; set; }
        public int Volume { get; set; }
        [ForeignKey("BatchId")]
        public Batch Batch { get; set; }
    }
}