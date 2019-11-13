using AutoMapper;
using NetTopologySuite.Geometries;
using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System.Linq;

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
                    .ForPath(d => d.Album, opt => opt.MapFrom(s => s.Album))
                    .ReverseMap()
                    .ForPath(s => s.Type, opt => opt.Ignore())
                    .ForPath(s => s.Coordinates.Coordinate.X, opt => opt.Ignore())
                    .ForPath(s => s.Coordinates.Coordinate.Y, opt => opt.Ignore())
                    .ForPath(s => s.Coordinates, o => o.MapFrom( s => new Point(s.Longitude, s.Latitude) { SRID = 4326 }))
                    .ForPath(s => s.Album, o => o.MapFrom( s => s.Album.ToList()));

                cnfg.CreateMap<SightType, SightTypeDTO>()
                    .ReverseMap();

                cnfg.CreateMap<Review, ReviewDTO>()
                    .ReverseMap()
                    .ForMember(r => r.Parent, memConf => memConf.Ignore());

                cnfg.CreateMap<Album, AlbumDTO>()
                    .ReverseMap();
            });

            return am_config;
        }
    }
}
