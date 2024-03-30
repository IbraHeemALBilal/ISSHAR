using AutoMapper;
using ISSHAR.Application.DTOs.HallImageDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class HallImageProfile : Profile
    {
        public HallImageProfile() 
        {
            CreateMap<HallImageDTO, HallImage>();
            CreateMap<HallImage, HallImageDisplayDTO>();
        }
    }
}
