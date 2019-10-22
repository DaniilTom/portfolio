using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class SightTypesDbManager : BaseDbManager<SightTypeDTO, SightTypeFilterDTO, SightType>
    {
        public SightTypesDbManager(ILogger<SightTypesDbManager> _logger, IRepository<SightType> _repo, IMapper _mapper) : base(_logger, _repo, _mapper) { }

        protected override IFilter<SightType> ConfigureFilter(SightTypeFilterDTO dto) => new SightTypeFilter(dto);
    }
}
