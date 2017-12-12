using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CoreDownloader;

namespace NeuroTradeAPI.Entities
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
        public DateTime BeginTime { get; set; }
        [ForeignKey("BatchId")]
        public Batch Batch { get; set; }

        public static Candle ConvertData(Data src, int batchId)
        {
            Candle c = new Candle {BatchId = batchId};
            try
            {
                c.Open = float.Parse(src._open, CultureInfo.InvariantCulture);
                c.Close = float.Parse(src._close, CultureInfo.InvariantCulture);
                c.Low = float.Parse(src._low, CultureInfo.InvariantCulture);
                c.High = float.Parse(src._high, CultureInfo.InvariantCulture);
                c.Volume = int.Parse(src._vol);
                c.BeginTime = DateTime.ParseExact(src._date + src._time, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            return c;
        }
        
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
                {"volume", Volume},
                {"per", Batch != null ? Batch.Interval.ToString() : "¯\\_(ツ)_/¯"}
            };
        }
    }
}