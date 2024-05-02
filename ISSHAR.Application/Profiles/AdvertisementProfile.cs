using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;

namespace ISSHAR.Application.Profiles
{
    public class AdvertisementProfile:Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<Advertisement, AdvertisementDisplayDTO>()
                            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<AdvertisementDisplayDTO, Advertisement>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Status>(src.Status))); 
            
            CreateMap<Advertisement, AdvertisementDTO>().ReverseMap();
        }
    }
}
