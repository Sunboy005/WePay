using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class UserWallet
    {       
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Address { get; set; }       
        public bool IsLocked { get; set; } = false;
               
        public string Pin { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime DateModified { get; set; } = DateTime.Now;
        
        public List<WalletCurrency> WalletCurrencies { get; } = new List<WalletCurrency> { };

        public string UserId { get; set; }

        public User User { get; set; } = null!;
            
            
    }
}
