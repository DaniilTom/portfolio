using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Mappers;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations.Test
{
    public class SightsDbAccess : BaseDbAccess<SightDTO, ShortSightDTO, Sight>
    {
        public SightsDbAccess(ILogger<SightsDbAccess> _logger, IRepository<Sight> _repo) : base(_logger, _repo) { }

        protected override Sight DtoToSource(SightDTO dto) => dto?.ToSource();

        protected override SightDTO SourceToDto(Sight item) => item?.ToDTO();

        protected override ShortSightDTO SourceToShortDto(Sight item) => item?.ToShortDTO();
    }
}
