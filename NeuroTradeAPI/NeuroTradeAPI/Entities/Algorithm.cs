using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI
{
    public class Algorithm
    {
        public int AlgorithmId { get; set; }
        public int UserId { get; set; }
        public string Path { get; set; }
        public string Password { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}