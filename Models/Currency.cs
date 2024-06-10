using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class Currency
    {
        [Key]
        public string CurrencyId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Symbol { get; set; }
        public List <WalletCurrency> WalletCurrencies { get; set; }
        public Currency()
        {
            WalletCurrencies = new List<WalletCurrency>();
        }
    }
}
