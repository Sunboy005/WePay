using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IUserService
    {


        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task<bool> LoginUser(UserForLoginDto userForLoginDto);
        Task<string> CreateToken();
        Task<bool> DeleteUser(UserDeletionDto userDeletionDto);
        Task<bool> UpdateUserAsync(string userId, UserUpdateDto userUpdateDto);

    }
}
