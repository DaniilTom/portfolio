using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers
{
    public class SightTypesController : BaseController<SightTypeDTO, SightTypeFilterDTO>
    { 
        public SightTypesController(IBaseManager<SightTypeDTO, SightTypeFilterDTO> manager) : base(manager) { }
    }
}