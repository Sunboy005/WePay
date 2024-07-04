using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace wepay.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();  
        public string TransactionReference { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;     
        public string WalletCurrencyId { get; set; }

        public WalletCurrency WalletCurrency { get; set; }       
        public string CurrencyName { get; set; }
        public int Amount { get; set; }
        public double ConversionRate { get; set; }
        public string TransactionType { get; set; }
    }
}
