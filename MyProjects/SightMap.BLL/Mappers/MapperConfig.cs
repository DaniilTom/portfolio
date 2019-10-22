using AutoMapper;
using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Mappers
{
    public static class MapperConfig
    {
        public static MapperConfiguration Initialize()
        {
            MapperConfiguration am_config = new MapperConfiguration(cnfg =>
            {
                cnfg.CreateMap<Sight, SightDTO>()
                    .ForMember(s => s.TypeName, opt => opt.MapFrom(d => d.Type.Name))
                    .ReverseMap()
                    .ForMember(d => d.Type, src => src.Ignore());

                cnfg.CreateMap<SightType, SightTypeDTO>()
                    .ReverseMap();
            });

            return am_config;
        }
    }
}
