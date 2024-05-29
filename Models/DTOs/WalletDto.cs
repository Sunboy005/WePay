using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace wepay.Models.DTOs
{
    public class WalletDto
    {
       
        public string WalletId { get; set; }
        public string Address { get; set; }
        
        public string UserId { get; set; }
        
        public bool IsLocked { get; set; } = false;
        
        public string Pin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
