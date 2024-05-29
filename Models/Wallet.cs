using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class Wallet
    {
        [Key]
        public string WalletId { get; set; } = Guid.NewGuid().ToString();
        public string Address { get; set; }
        [Column("Id")]
        [ForeignKey("Id")]
        public string UserId { get; set; }
        public User? user { get; set; }
        public bool IsLocked { get; set; } = false;
        public string Pin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
