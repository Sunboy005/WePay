using AutoMapper;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserCreationDto, User>();
            CreateMap<UserForRegistrationDto, UserCreationDto>();
            CreateMap<UserCreationDto, UserForRegistrationDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, IdentityUserDto>();
        }
    }
}
