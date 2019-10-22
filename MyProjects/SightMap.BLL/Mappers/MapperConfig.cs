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
                    .ForMember(d => d.TypeName, opt => opt.MapFrom(s => s.Type.Name))
                    .ReverseMap()
                    .ForMember(s => s.Type, memConf => memConf.Ignore());

                cnfg.CreateMap<SightType, SightTypeDTO>()
                    .ReverseMap();

                cnfg.CreateMap<Review, ReviewDTO>()
                    .ReverseMap()
                    .ForMember(r => r.Parent, memConf => memConf.Ignore());
            });

            return am_config;
        }
    }
}
