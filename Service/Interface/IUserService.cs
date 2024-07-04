using Microsoft.AspNetCore.Identity;

using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<IdentityResult> RegisterAdmin(AdminForRegistrationDto adminForRegistrationDto);
        Task<User?> GetUserById(string id);
        Task<User?> GetUserByEmail(string email);
        Task DeleteUser(UserDeletionDto userDeletionDto);
        Task UpdateUserAsync(string userId, UserUpdateDto userUpdateDto);
        Task<string> GetRoleOfUser(User user);
    }
}
