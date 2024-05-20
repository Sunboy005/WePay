using AutoMapper;

namespace wepay.Models
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserForRegistrationDto, User>();

        }
    }
}
