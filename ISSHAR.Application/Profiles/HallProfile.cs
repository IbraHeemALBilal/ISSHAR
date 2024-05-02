using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;

namespace ISSHAR.Application.Profiles
{
    public class HallProfile : Profile
    {
        public HallProfile() 
        {
            CreateMap<Hall, HallDisplayDTO>()
                            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); 

            CreateMap<HallDisplayDTO, Hall>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Status>(src.Status)));

            CreateMap<Hall, HallDTO>().ReverseMap();
        }
    }
}
