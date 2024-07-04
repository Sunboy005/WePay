using wepay.Models;

namespace wepay.Models
{
    public class WalletCurrencyDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsBase { get; set; } = false;
        public int Balance { get; set; } = 0;        
        public string WalletId { get; set; }
        public string CurrencyId { get; set; }
        public CurrencyDto Currency { get; set; }    
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;

    }
}


