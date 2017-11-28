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
    //Have no idea how to fix
//    public override string ToString()
//    {
//        return String.Format("{0}. Ticker: {1}, start time: {2:hh:mm:ss dd/MM/yyyy}, interval: {3}",
//                BatchId, Alias, Timestamp, Interval);
//    }  
}