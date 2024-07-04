using Microsoft.AspNetCore.Identity;

using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface IAuthService
    {
        Task<bool> LoginUser(UserForLoginDto userForLoginDto);     
              
        Task<string> CreateToken();      

        Task LogoutUser();
    }
}
