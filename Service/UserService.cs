using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wepay.EmailService;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
     

        private User _user;

        public UserService(UserManager<User> userManager, IMapper mapper)

        {
           
            _mapper = mapper;
            _userManager = userManager;
            
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var userForCreation = _mapper.Map<UserCreationDto>(userForRegistrationDto);

            var user = _mapper.Map<User>(userForCreation);

            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            return result;

        }

     

            public async Task<bool> DeleteUser(UserDeletionDto userDeletionDto)
            {


                var user = await _userManager.FindByEmailAsync(userDeletionDto.Email);
                if (user == null)
                {
                    return false;
                }

                var passwordCorrect = await _userManager.CheckPasswordAsync(user, userDeletionDto.Password);

                if (passwordCorrect == false)
                {
                    return false;
                }

                var result = await _userManager.DeleteAsync(user);

                return result.Succeeded;

            }

        public async Task<bool> UpdateUserAsync(string userId, UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            if (userUpdateDto.Email != null)
                user.Email = userUpdateDto.Email;
            if (userUpdateDto.PhoneNumber != null)
                user.PhoneNumber = userUpdateDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<IdentityUserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var identityUser = _mapper.Map<IdentityUserDto>(user);
            identityUser.Roles = roles;
            return identityUser;
        }
        public async Task<IdentityUserDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var identityUser = _mapper.Map<IdentityUserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            identityUser.Roles = roles;

            return identityUser;
        }

    }
}

