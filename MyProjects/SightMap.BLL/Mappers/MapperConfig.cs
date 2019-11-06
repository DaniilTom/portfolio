using AutoMapper;
using NetTopologySuite.Geometries;
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
                    .ForPath(d => d.Longitude, opt => opt.MapFrom(s => s.Coordinates.Coordinate.X))
                    .ForPath(d => d.Latitude, opt => opt.MapFrom(s => s.Coordinates.Coordinate.Y))
                    .ReverseMap()
                    .ForPath(s => s.Type, opt => opt.Ignore())
                    .ForPath(s => s.Coordinates.Coordinate.X, opt => opt.Ignore())
                    .ForPath(s => s.Coordinates.Coordinate.Y, opt => opt.Ignore())
                    .ForPath(s => s.Coordinates, o => o.MapFrom( s => new Point(s.Longitude, s.Latitude) { SRID = 4326 }));

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
