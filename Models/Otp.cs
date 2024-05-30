using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class Otp
    {
        [Key]
        public string OtpId { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpired { get; set; }

        public bool IsExpired { get; set; }
        
        [ForeignKey("Email")]
        public string Email { get; set; }
        public User user { get; set; }
        public string Reason { get; set; }

    }
}
