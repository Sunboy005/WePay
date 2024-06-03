using Microsoft.AspNetCore.Identity;

using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<IdentityResult> RegisterAdmin(AdminForRegistrationDto adminForRegistrationDto);
        Task<IdentityUserDto?> GetUserById(string id);
        Task<IdentityUserDto?> GetUserByEmail(string email);
        Task<bool> DeleteUser(UserDeletionDto userDeletionDto);
        Task<bool> UpdateUserAsync(string userId, UserUpdateDto userUpdateDto);
        Task<string> GetRoleOfUser(User user);
    }
}
