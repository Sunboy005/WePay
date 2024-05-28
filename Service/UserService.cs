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

            user.CreatedDate = DateTime.Now;

            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForCreation.Role);
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
            if (userUpdateDto.PhoneNumber != null)
                user.PhoneNumber = userUpdateDto.PhoneNumber;
            if (userUpdateDto.FirstName != null)
                user.PhoneNumber = userUpdateDto.FirstName;
            if (userUpdateDto.LastName != null)
                user.PhoneNumber = userUpdateDto.LastName;
            if (userUpdateDto.Address != null)
                user.PhoneNumber = userUpdateDto.Address;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<IdentityUserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var identityUser = _mapper.Map<IdentityUserDto>(user);
            identityUser.Role = roles.First();
            return identityUser;
        }
        public async Task<IdentityUserDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var identityUser = _mapper.Map<IdentityUserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            identityUser.Role = roles.First();

            return identityUser;
        }

        public async Task<IdentityResult> RegisterAdmin(AdminForRegistrationDto adminForRegistrationDto)
        {
            var adminForCreation = _mapper.Map<AdminCreationDto>(adminForRegistrationDto);

            var admin = _mapper.Map<User>(adminForCreation);

            admin.CreatedDate = DateTime.Now;

            var result = await _userManager.CreateAsync(admin, adminForRegistrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(admin, adminForCreation.Role);
            return result;
        }
    }
}

