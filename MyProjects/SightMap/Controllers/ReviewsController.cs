using Microsoft.AspNetCore.Mvc;
using SightMap.Attributes;
using SightMap.BLL.DTO;
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
        private IDbManager<ReviewDTO, ReviewFilterDTO> dataStore;

        public ReviewsController(IDbManager<ReviewDTO, ReviewFilterDTO> _dataStore)
        {
            dataStore = _dataStore;
        }

        [HttpPost]
        public ResultState<ReviewDTO> Post([FromBody] ReviewDTO dto)
        {
            var resultObject = dataStore.Add(dto);
            var resultState = ResultState<ReviewDTO>.CreateResulState<ReviewDTO>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<ReviewDTO> Put(ReviewDTO dto)
        {
            var resultObject = dataStore.Edit(dto);
            var resultState = ResultState<ReviewDTO>.CreateResulState<ReviewDTO>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<ReviewDTO> Delete(int id)
        {
            var success = dataStore.Delete(id);
            ResultState<ReviewDTO> resultState = new ResultState<ReviewDTO>();
            if (success)
                resultState.IsSuccess = true;
            else
            {
                resultState.IsSuccess = false;
                resultState.Message = "Что-то пошло не так.";
            }

            return resultState;
        }

        [HttpGet]
        public ResultState<IEnumerable<ReviewDTO>> Get([FromQuery] ReviewFilterDTO filter)
        {
            var resultObject = dataStore.GetListObjects(filter);
            var resultState = ResultState<IEnumerable<ReviewDTO>>.CreateResulState<IEnumerable<ReviewDTO>>(resultObject);

            return resultState;
        }

        [HttpGet]
        public ResultState<ReviewDTO> Get([RequiredFromQuery]int id)
        {
            var resultObject = dataStore.GetListObjects(new ReviewFilterDTO { Id = id });
            var resultState = ResultState<ReviewDTO>.CreateResulState<ReviewDTO>(resultObject?.FirstOrDefault());

            return resultState;
        }
    }
}
