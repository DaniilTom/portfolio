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
    public class SightsController : Controller
    {
        private IBaseManager<SightDTO, SightFilterDTO> _manager;

        public SightsController(IBaseManager<SightDTO, SightFilterDTO> manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public ResultState<SightDTO> Post([FromBody] SightDTO dto)
        {
            var resultObject = _manager.Add(dto);
            var resultState = new ResultState<SightDTO>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<SightDTO> Put(SightDTO dto)
        {
            var resultObject = _manager.Edit(dto);
            var resultState = new ResultState<SightDTO>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<SightDTO> Delete(int id)
        {
            var success = _manager.Delete(id);
            ResultState<SightDTO> resultState = new ResultState<SightDTO>(null);
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
        public ResultState<IEnumerable<ShortSightDTO>> Get([FromQuery] SightFilterDTO filter)
        {
            var resultObject = _manager.GetListObjects(filter);
            var resultState = new ResultState<IEnumerable<ShortSightDTO>>(resultObject);

            return resultState;
        }

        [HttpGet]
        public ResultState<SightDTO> Get([RequiredFromQuery]int id)
        {
            var resultObject = _manager.GetListObjects(new SightFilterDTO { Id = id });
            var resultState = new ResultState<SightDTO>(resultObject.FirstOrDefault());

            return resultState;
        }
    }
}
