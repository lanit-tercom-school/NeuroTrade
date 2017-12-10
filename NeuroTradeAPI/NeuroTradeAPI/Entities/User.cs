using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroTradeAPI.Entities
{
    public class User
    {   
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}