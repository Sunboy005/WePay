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
        public bool IsBase { get; set; } = false;
        public int Balance { get; set; } = 0;
        public string Symbol { get; set; }
        
        [ForeignKey("WalletId")]
        public string WalletId { get; set; }
        public Wallet? wallet { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
