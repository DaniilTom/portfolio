using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.CustomCache;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class AlbumDbManager : BaseDbManager<AlbumDTO, AlbumFilterDTO, Album>
    {
        public AlbumDbManager(ILogger<AlbumDbManager> _logger,
                                  IRepository<Album> _repo,
                                  IMapper _mapper,
                                  ICustomCache _cache) : base(_logger, _repo, _mapper, _cache) { }

        protected override IFilter<Album> ConfigureFilter(AlbumFilterDTO dto) => new AlbumFilter(dto);
    }
}
