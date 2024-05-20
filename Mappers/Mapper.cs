using AutoMapper;
using wepay.Models;

namespace wepay.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserForRegistrationDto, User>();

        }
    }
}
