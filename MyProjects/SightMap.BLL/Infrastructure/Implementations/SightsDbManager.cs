using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class SightsDbManager : BaseDbManager<SightDTO, ShortSightDTO, SightFilterDTO, Sight>
    {
        public SightsDbManager(ILogger<SightsDbManager> _logger, IRepository<Sight> _repo, IMapper _mapper) : base(_logger, _repo, _mapper) { }
        protected override IFilter<Sight> ConfigureFilter(SightFilterDTO dto) => new SightFilter(dto);
    }
}
