using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers.Api
{
    public class ReviewsController : BaseApiController<ReviewDTO, ReviewFilterDTO>
    {
        public ReviewsController(IBaseManager<ReviewDTO, ReviewFilterDTO> manager) : base(manager) { }
    }
}
