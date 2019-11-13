using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.CustomCache;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class SightTypesManager : BaseManager<SightTypeDTO, SightTypeFilterDTO, SightType>
    {
        public SightTypesManager(ILogger<SightTypesManager> _logger,
                                   IRepository<SightType> _repo,
                                   IMapper _mapper,
                                   ICustomCache _cache) : base(_logger, _repo, _mapper, _cache) { }

        protected override IFilter<SightType> ConfigureFilter(SightTypeFilterDTO dto) => new SightTypeFilter(dto);
    }
}
