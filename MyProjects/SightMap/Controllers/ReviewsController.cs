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
    public class ReviewsController : Controller
    {
        private IBaseManager<ReviewDTO, ReviewFilterDTO> _manager;

        public ReviewsController(IBaseManager<ReviewDTO, ReviewFilterDTO> manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public ResultState<ReviewDTO> Post([FromBody] ReviewDTO dto)
        {
            var resultObject = _manager.Add(dto);
            var resultState = new ResultState<ReviewDTO>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<ReviewDTO> Put(ReviewDTO dto)
        {
            var resultObject = _manager.Edit(dto);
            var resultState = new ResultState<ReviewDTO>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<ReviewDTO> Delete(int id)
        {
            var success = _manager.Delete(id);
            ResultState<ReviewDTO> resultState = new ResultState<ReviewDTO>(null);
            if (success)
                resultState.IsSuccess = true;
            else
            {
                resultState.IsSuccess = false;
                resultState.Message = Constants.ErrorIdWrong;
            }

            return resultState;
        }

        [HttpGet]
        public ResultState<IEnumerable<ReviewDTO>> Get([FromQuery] ReviewFilterDTO filter)
        {
            var resultObject = _manager.GetListObjects(filter);
            var resultState = new ResultState<IEnumerable<ReviewDTO>>(resultObject);

            return resultState;
        }

        [HttpGet]
        public ResultState<ReviewDTO> Get([RequiredFromQuery]int id)
        {
            var resultObject = _manager.GetListObjects(new ReviewFilterDTO { Id = id });
            var resultState = new ResultState<ReviewDTO>(resultObject.FirstOrDefault());

            return resultState;
        }
    }
}
