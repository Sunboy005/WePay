using Microsoft.AspNetCore.Identity;
using wepay.Models;

namespace wepay.Service.Interface
{
    public interface IUserService
    {
<<<<<<< HEAD

        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        

=======
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
>>>>>>> 9af1d20c4e9b8549e9a8c18e1d88120a8bce5026
    }
}
