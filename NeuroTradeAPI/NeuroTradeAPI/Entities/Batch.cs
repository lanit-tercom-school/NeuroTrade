using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI.Entities
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

        public static TimeSpan StrToInterval(string finam_form)
        {
            if (int.TryParse(finam_form, out var num))
                return new TimeSpan(0, num, 0);
            return finam_form[0] == 'D' ? new TimeSpan(1, 0, 0, 0) :
                   finam_form[0] == 'W' ? new TimeSpan(7, 0, 0, 0) :
                   finam_form[0] == 'M' ? new TimeSpan(30, 12, 0, 0) :
                   throw new ArgumentException("Doesn't match Finam's 'per' format");


        }
    }
}