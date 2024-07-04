using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wepay.Models
{
    public class CurrencyDto
    {        
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ShortCode { get; set; }               
        public string Symbol { get; set; }          
    }
}
