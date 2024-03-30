using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.DTOs.BookingDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDisplayDTO>().ReverseMap();
            CreateMap<Booking, BookingDTO>().ReverseMap();
        }

    }
}
