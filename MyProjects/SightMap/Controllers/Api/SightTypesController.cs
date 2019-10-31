using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers.Api
{
    public class SightTypesController : BaseApiController<SightTypeDTO, SightTypeFilterDTO>
    { 
        public SightTypesController(IBaseManager<SightTypeDTO, SightTypeFilterDTO> manager) : base(manager) { }
    }
}
