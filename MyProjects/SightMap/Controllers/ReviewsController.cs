using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;

namespace SightMap.Controllers
{
    public class ReviewsController : BaseController<ReviewDTO, ReviewFilterDTO>
    {
        public ReviewsController(IBaseManager<ReviewDTO, ReviewFilterDTO> manager) : base(manager) { }
    }
}
