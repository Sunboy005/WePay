using Microsoft.AspNetCore.Identity;
using wepay.EmailService;
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

        Task<(bool, IdentityResult)> ChangePassword(UserForChangePasswordDto userForChangePasswordDto);

        Task<String> VerifyUserEmail(string email, string url);

        Task SendEmailAsync(Message mesage);

        Task<IdentityResult> ConfirmUserEmail(UserForEmailConfirmationDto userForEmailConfirmationDto);

    }
}
