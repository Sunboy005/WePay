using Microsoft.AspNetCore.Identity;
using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IUserService
    {


        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task<bool> LoginUser(UserForLoginDto userForLoginDto);

        Task<string> CreateToken();

    }
}
