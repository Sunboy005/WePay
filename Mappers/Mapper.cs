using AutoMapper;
using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserCreationDto, UserForRegistrationDto>();

        }
    }
}
