using AutoMapper;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;

namespace magicVilla_VillaAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig() 
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            CreateMap<Villa,VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreatedDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();

        }
    }
}
