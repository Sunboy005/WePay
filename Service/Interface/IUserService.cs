using Microsoft.AspNetCore.Identity;
using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IUserService
    {
<<<<<<< HEAD
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<User> GetUserById(Guid id);
=======
        
>>>>>>> e06645cbc996d52b330e44d26e4e745bf8f4a647
    }
}
