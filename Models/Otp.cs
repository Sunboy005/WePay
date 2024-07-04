using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class Otp
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpired { get; set; }
        public bool IsExpired { get; set; }
            
        public string UserId { get; set; }
        public User User { get; set; }
        public string Reason { get; set; }

    }
}
