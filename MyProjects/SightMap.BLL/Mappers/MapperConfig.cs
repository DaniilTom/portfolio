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
                    .ForPath(d => d.Type.Id, opt => opt.MapFrom(s => s.SightTypeId))
                    .ReverseMap()
                    .ForPath(s => s.Type, memConf => memConf.Ignore());

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
