using Microsoft.Extensions.Hosting;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers.Api
{
    public class ReviewsController : BaseApiController<ReviewDTO, ReviewFilterDTO>
    {
        public ReviewsController(IBaseManager<ReviewDTO, ReviewFilterDTO> manager, IHostEnvironment host) : base(manager, host) { }
    }
}
