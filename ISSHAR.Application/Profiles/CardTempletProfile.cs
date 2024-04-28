using AutoMapper;
using ISSHAR.Application.DTOs.CardTempletDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Profiles
{
    public class CardTempletProfile : Profile
    {
        public CardTempletProfile() 
        {
            CreateMap<CardTemplet, CardTempletDisplayDTO>().ReverseMap();
            CreateMap<CardTemplet, CardTempletDTO>().ReverseMap();
        }
    }
}
