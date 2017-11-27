using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI
{
    public class Batch
    {
        public int BatchId { get; set; }
        public string Alias { get; set; }
        public TimeSpan Interval { get; set; }
        public DateTime Timestamp { get; set; }
    }
}