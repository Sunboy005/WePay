using Microsoft.AspNetCore.Identity;
using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IUserService
    {

        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        

    }
}
