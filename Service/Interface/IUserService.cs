using Microsoft.AspNetCore.Identity;

namespace wepay.Service.Interface
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        
    }
}
