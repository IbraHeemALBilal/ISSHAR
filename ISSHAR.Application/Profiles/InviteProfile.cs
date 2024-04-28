using AutoMapper;
using ISSHAR.Application.DTOs.InviteDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class InviteProfile : Profile
    {
        public InviteProfile() 
        {
            CreateMap<Invite, InviteDisplayDTO>().ReverseMap();
            CreateMap<Invite, InviteDTO>().ReverseMap();
        }
    }
}
