using Microsoft.AspNetCore.Identity;

namespace wepay.Models
{
    public class User : IdentityUser 
    {
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
