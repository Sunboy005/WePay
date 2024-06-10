using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class Wallet
    {
        [Key]
        public string WalletId { get; set; } = Guid.NewGuid().ToString();
        public string Address { get; set; }
        public string UserId { get; set; }
        public User? user { get; set; }
        public bool IsLocked { get; set; } = false;
        [Required(ErrorMessage = "Pin is required")]
        [StringLength(4, ErrorMessage = "Pin must be 4 digits")]
        public string Pin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
        public List<WalletCurrency> WalletCurrencies { get; set; }
        public Wallet()
        {
            WalletCurrencies = new List<WalletCurrency>();   
        }
    }
}
