using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Mappers;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations.Test
{
    public class SightTypesDbManager : BaseDbManager<SightTypeDTO, SightTypeDTO, SightTypeFilterDTO, SightType>
    {
        public SightTypesDbManager(ILogger<SightsDbManager> _logger, IRepository<SightType> _repo, IMapper _mapper) : base(_logger, _repo, _mapper) { }

        protected override SightType DtoToSource(SightTypeDTO dto) => dto?.ToSource();

        protected override SightTypeDTO SourceToDto(SightType item) => item?.ToDTO();

        protected override SightTypeDTO SourceToShortDto(SightType item) => item?.ToShortDTO();

        protected override IFilter<SightType> ConfigureFilter(SightTypeFilterDTO dto) => new SightTypeFilter(dto);
    }
}
