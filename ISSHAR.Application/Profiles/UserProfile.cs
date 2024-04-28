using AutoMapper;
using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserDisplayDTO>().ReverseMap();
            CreateMap<User, SenderDTO>().ReverseMap();

        }
    }
}