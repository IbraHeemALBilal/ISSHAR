using AutoMapper;
using ISSHAR.Application.DTOs.CardDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    internal class CardProfile : Profile
    {
        public CardProfile() 
        {
            CreateMap<Card, CardDisplayDTO>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
        }
    }
}
