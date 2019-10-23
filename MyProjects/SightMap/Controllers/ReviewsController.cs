using Microsoft.AspNetCore.Mvc;
using SightMap.Attributes;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System.Collections.Generic;
using System.Linq;

namespace SightMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController<ReviewDTO, ReviewFilterDTO>
    {
        public ReviewsController(IBaseManager<ReviewDTO, ReviewFilterDTO> manager) : base(manager) { }
    }
}
