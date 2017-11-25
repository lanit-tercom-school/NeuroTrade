using System;

namespace Downloader
{
    /// <summary>
    /// Class for saving data; waiting for database response to adjust the fromat
    /// there are all types of data that i have met, may be there are others type - need to figure out it
    /// </summary>
    public class Data
    {
        public static int NumberOfFields { get; } = 9;

        private string _ticker;
        private string _per;
        private string _date;
        private string _time;
        private string _open;
        private string _high;
        private string _low;
        private string _close;
        private string _vol;

        public Data()
        {
            _ticker = null;
            _per = null;
            _date = null;
            _time = null;
            _open = null;
            _high = null;
            _low = null;
            _close = null;
            _vol = null;
        }

        
        /// <summary>
        /// to set some of fields
        /// </summary>
        /// <param name="fields"> fields[i] == -1 means that i-th field is null; else i-th field = value[fields[i]</param>
        /// <param name="value"> value of fields </param>
        public void setSomeOfFields(int[] fields, string[] value)
        {                 
            _ticker = setField(0, fields, value);
            _per = setField(1, fields, value);
            _date = setField(2, fields, value);
            _time = setField(3, fields, value);
            _open = setField(4, fields, value);
            _high = setField(5, fields, value);
            _low = setField(6, fields, value);
            _close = setField(7, fields, value);
            _vol = setField(8, fields, value);
        }

        private string setField(int i, int[] fields, string[] value) => fields[i] == -1 ? null : value[fields[i]];
       
        
        public void print()
        {
            Console.Write($"ticker {valueToPrint(_ticker)}, per {valueToPrint(_per)}, date {valueToPrint(_date)}," +
                          $" time {valueToPrint(_time)}, open {valueToPrint(_open)}, high {valueToPrint(_high)}," +
                          $" low {valueToPrint(_low)}, close {valueToPrint(_close)}, vol {valueToPrint(_vol)}");
        }
        
        private string valueToPrint(string field) => field == null ? "null" : field;
    }
}