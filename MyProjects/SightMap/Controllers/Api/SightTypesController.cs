using Microsoft.Extensions.Hosting;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers.Api
{
    public class SightTypesController : BaseApiController<SightTypeDTO, SightTypeFilterDTO>
    { 
        public SightTypesController(IBaseManager<SightTypeDTO, SightTypeFilterDTO> manager, IHostEnvironment host) : base(manager, host) { }
    }
}
