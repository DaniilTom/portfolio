using Microsoft.AspNetCore.Mvc;
using SightMap.Attributes;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System.Collections.Generic;

namespace SightMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightsController : Controller
    {
        private IDbManager<SightDTO, ShortSightDTO, SightFilterDTO> dataStore;

        public SightsController(IDbManager<SightDTO, ShortSightDTO, SightFilterDTO> _dataStore)
        {
            dataStore = _dataStore;
        }

        [HttpPost]
        public ResultState<SightDTO> Post([FromBody] SightDTO dto)
        {
            var resultObject = dataStore.Add(dto);
            var resultState = ResultState<SightDTO>.CreateResulState<SightDTO>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<SightDTO> Put(SightDTO dto)
        {
            var resultObject = dataStore.Edit(dto);
            var resultState = ResultState<SightDTO>.CreateResulState<SightDTO>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<SightDTO> Delete(int id)
        {
            var success = dataStore.Delete(id);
            ResultState<SightDTO> resultState = new ResultState<SightDTO>();
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
        public ResultState<IEnumerable<ShortSightDTO>> Get([FromQuery] SightFilterDTO filter)
        {
            var resultObject = dataStore.GetListObjects(filter);
            var resultState = ResultState<IEnumerable<ShortSightDTO>>.CreateResulState<IEnumerable<ShortSightDTO>>(resultObject);

            return resultState;
        }

        [HttpGet]
        public ResultState<SightDTO> Get([RequiredFromQuery]int id)
        {
            var resultObject = dataStore.GetObject(id);
            var resultState = ResultState<SightDTO>.CreateResulState<SightDTO>(resultObject);

            return resultState;
        }
    }
}
