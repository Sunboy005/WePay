using Microsoft.AspNetCore.Identity;
using wepay.EmailService;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IAuthService
    {
        Task<bool> LoginUser(UserForLoginDto userForLoginDto);
        Task<(bool, IdentityResult)> ChangePassword(UserForChangePasswordDto userForChangePasswordDto);

        Task<String> VerifyUserEmail(string email, string url);

        Task SendEmailAsync(Message mesage);

        Task<IdentityResult> ConfirmUserEmail(UserForEmailConfirmationDto userForEmailConfirmationDto);
        Task<string> CreateToken();
    }
}
