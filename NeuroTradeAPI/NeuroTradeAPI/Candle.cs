namespace NeuroTradeAPI
{
    public class Candle
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public float Open { get; set; }
        public float Close { get; set; }
        public float Low { get; set; }
        public float High { get; set; }
        public int Volume { get; set; }
    }
}