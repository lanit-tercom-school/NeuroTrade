using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI.Entities
{
    public class Algorithm
    {
        public int AlgorithmId { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}