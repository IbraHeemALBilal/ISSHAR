using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class HallProfile : Profile
    {
        public HallProfile() 
        {
            CreateMap<Hall, HallDisplayDTO>().ReverseMap();
            CreateMap<Hall, HallDTO>().ReverseMap();
        }
    }
}
