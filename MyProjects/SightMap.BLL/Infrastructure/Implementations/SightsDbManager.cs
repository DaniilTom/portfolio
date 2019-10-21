using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Mappers;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations.Test
{
    public class SightsDbManager : BaseDbManager<SightDTO, ShortSightDTO, SightFilterDTO, Sight>
    {
        public SightsDbManager(ILogger<SightsDbManager> _logger, IRepository<Sight> _repo, IMapper _mapper) : base(_logger, _repo, _mapper) { }

        protected override Sight DtoToSource(SightDTO dto) => dto?.ToSource();

        protected override SightDTO SourceToDto(Sight item) => item?.ToDTO();

        protected override ShortSightDTO SourceToShortDto(Sight item) => item?.ToShortDTO();

        protected override IFilter<Sight> ConfigureFilter(SightFilterDTO dto) => new SightFilter(dto);
    }
}
