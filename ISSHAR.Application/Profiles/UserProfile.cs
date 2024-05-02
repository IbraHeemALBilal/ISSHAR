using AutoMapper;
using CloudinaryDotNet.Actions;
using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)));

            CreateMap<User, UserDisplayDTO>().ReverseMap();
            CreateMap<User, UserInfoDTO>().ReverseMap();

        }
    }
}