using AutoMapper;
using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User>  _userManager;
        private readonly IMapper _mapper;

        private User _user;

        public UserService(UserManager<User> userManager, IMapper mapper)

        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            return result;

        }
        public async Task<User> GetUserById(Guid id)
        {
            var user= _userManager.FindByIdAsync(id); 
            return user;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            return user;
        }
    }
}
