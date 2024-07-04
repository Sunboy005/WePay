using AutoMapper;
using Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;        

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
                {
                await _userManager.AddToRolesAsync(user, userForCreation.Role); }
            return result;

        }



        public async Task DeleteUser(UserDeletionDto userDeletionDto)
        {
            var user = await _userManager.FindByEmailAsync(userDeletionDto.Email);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, userDeletionDto.Password);

            if (passwordCorrect == false)
            {
                throw new BadRequestException("Incorrect Password");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded == false)
            {
                throw new InternalServerErrorException("An error occured");
            }


        }

        public async Task UpdateUserAsync(string userId, UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }
            if (userUpdateDto.PhoneNumber != null)
                user.PhoneNumber = userUpdateDto.PhoneNumber;
            if (userUpdateDto.FirstName != null)
                user.FirstName = userUpdateDto.FirstName;
            if (userUpdateDto.LastName != null)
                user.LastName = userUpdateDto.LastName;
            if (userUpdateDto.Address != null)
                user.Address = userUpdateDto.Address;
            if (userUpdateDto.ProfilePicture != null)
                user.ProfilePicture = userUpdateDto.ProfilePicture;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded == false)
            {
                throw new InternalServerErrorException("An error occurred");
            }
        }
        public async Task<User?> GetUserById(string id)
        {
            var user =  _userManager.Users.Include(u => u.Wallet).SingleOrDefault(u => u.Id == id);
            return user;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var user = _userManager.Users.Include(u => u.Wallet).SingleOrDefault(u => u.Email == email);
            return user;
        }      
        public async Task<IdentityResult> RegisterAdmin(AdminForRegistrationDto adminForRegistrationDto)
        {
            var adminForCreation = _mapper.Map<AdminCreationDto>(adminForRegistrationDto);

            var admin = _mapper.Map<User>(adminForCreation);

            admin.CreatedDate = DateTime.Now;

            var result = await _userManager.CreateAsync(admin, adminForRegistrationDto.Password);

            if (result.Succeeded)
              { await _userManager.AddToRolesAsync(admin, adminForCreation.Role); }
            return result;
        }

        public async Task<string> GetRoleOfUser(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.First();
        }

    }
}

