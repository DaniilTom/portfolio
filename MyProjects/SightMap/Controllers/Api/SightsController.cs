using Microsoft.Extensions.Hosting;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers.Api
{
    public class SightsController : BaseApiController<SightDTO, SightFilterDTO>
    {
        public SightsController(IBaseManager<SightDTO, SightFilterDTO> manager, IHostEnvironment host) : base(manager,host) { }
    }
}
