using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class AdvertisementProfile:Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<Advertisement, AdvertisementDisplayDTO>().ReverseMap();
            CreateMap<Advertisement, AdvertisementDTO>().ReverseMap();
        }
    }
}
