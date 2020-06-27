using AutoMapper;
using GeoService.API.Models;
using GeoService.BLL.DTO;

namespace GeoService.API.Extension
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, ProfileModel>();
            CreateMap<GeoParameterDTO, FixPointModel>();
        }
    }
}
