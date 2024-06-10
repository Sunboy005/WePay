using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace wepay.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string TransactionReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
        public string WalletId { get; set; }
        public string CurrencyId { get; set; }
        public int Amount { get; set; }
        public double ConversionRate { get; set; }
        public string TransactionType { get; set; }
    }
}
