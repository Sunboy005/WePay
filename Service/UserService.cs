﻿using AutoMapper;
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
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        private User _user;

        public UserService(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IEmailSender emailSender)

        {
            _emailSender = emailSender;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
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
        public async Task<bool> LoginUser(UserForLoginDto userForLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            if (_user == null)
            {
                return false;
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(_user, userForLoginDto.Password);

            if (!passwordCorrect)
            {
                return false;
            }
            return true;
        }

        public async Task<(bool, IdentityResult)> ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(userForChangePasswordDto.Email);
            if (user == null) {
                return (false, IdentityResult.Failed());
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token == null)
            {
                return (false, IdentityResult.Failed());
            }

           var result = await _userManager.ResetPasswordAsync(user, token, userForChangePasswordDto.NewPassword);

          

            if (!result.Succeeded)
            {
                return (false, result);
            }
            return (true, result);
        }



        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {

            var key = Encoding.UTF8.GetBytes("OUR_WEPAY_VERY_SECRET_KEY_THAT_SHOULD_NOT_LEAK_OUTSIDE_THIS_ENVIRONMENT");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        public async Task SendEmailAsync(Message message)
        {
            await _emailSender.SendEmailAsync(message);

        }


        public async Task<String> VerifyUserEmail(String email, string url)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not Found");
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (token == null)
            {
                throw new Exception("Internal Server Error");
            }
             return token;

        }

        public async Task<IdentityResult> ConfirmUserEmail(UserForEmailConfirmationDto userForEmailConfirmationDto)
        {
            var user = await _userManager.FindByEmailAsync(userForEmailConfirmationDto.Email);
            if (user == null)
            {
                throw new Exception("User not Found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, userForEmailConfirmationDto.Token);
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

    }
}

