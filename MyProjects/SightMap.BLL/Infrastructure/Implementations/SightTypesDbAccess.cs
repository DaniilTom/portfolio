using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Mappers;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations.Test
{
    public class SightTypesDbAccess : BaseDbAccess<SightTypeDTO, SightTypeDTO, SightType>
    {
        public SightTypesDbAccess(ILogger<SightsDbAccess> _logger, IRepository<SightType> _repo) : base(_logger, _repo) { }

        protected override SightType DtoToSource(SightTypeDTO dto) => dto?.ToSource();

        protected override SightTypeDTO SourceToDto(SightType item) => item?.ToDTO();

        protected override SightTypeDTO SourceToShortDto(SightType item) => item?.ToShortDTO();
    }
}
