using Microsoft.AspNetCore.Identity;

using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IAuthService
    {
        Task<bool> LoginUser(UserForLoginDto userForLoginDto);
        Task<IdentityResult> ChangePassword(string email, string newPassword);
       
        Task<IdentityResult> ConfirmUserEmail(UserForEmailConfirmationDto userForEmailConfirmationDto);
        Task<string> CreateToken();

        Task LogoutUser();
    }
}
