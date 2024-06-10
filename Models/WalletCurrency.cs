using wepay.Models;

namespace wepay.Models
{
    public class WalletCurrency
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Wallet WalletId { get; set; }
        public Currency CurrencyId { get; set; }
        public bool IsBase { get; set; }
        public int Balance { get; set; }
        public Wallet wallet { get; set; }
        public Currency currency { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}


