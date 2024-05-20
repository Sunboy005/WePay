using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User>  _userManager;

        private User _user;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }
}
