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
            CreateMap<VillaDTO,VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<VillaCreateDTO, Villa>(); 
        }
    }
}
