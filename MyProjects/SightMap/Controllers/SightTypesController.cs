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
    public class SightTypesController : Controller
    {
        private IBaseManager<SightTypeDTO, SightTypeFilterDTO> _manager;
        public SightTypesController(IBaseManager<SightTypeDTO, SightTypeFilterDTO> manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public ResultState<SightTypeDTO> Post([FromBody] SightTypeDTO dto)
        {
            var resultObject = _manager.Add(dto);
            var resultState = new ResultState<SightTypeDTO>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<SightTypeDTO> Put(SightTypeDTO dto)
        {
            var resultObject = _manager.Edit(dto);
            var resultState = new ResultState<SightTypeDTO>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<SightTypeDTO> Delete(int id)
        {
            var success = _manager.Delete(id);
            ResultState<SightTypeDTO> resultState = new ResultState<SightTypeDTO>(null);
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
        public ResultState<IEnumerable<SightTypeDTO>> Get([FromQuery] SightTypeFilterDTO filter)
        {
            var resultObject = _manager.GetListObjects(filter);
            var resultState = new ResultState<IEnumerable<SightTypeDTO>>(resultObject);

            return resultState;
        }

        [HttpGet]
        public ResultState<SightTypeDTO> Get([RequiredFromQuery]int id)
        {
            var resultObject = _manager.GetListObjects(new SightTypeFilterDTO { Id = id });
            var resultState = new ResultState<SightTypeDTO>(resultObject?.FirstOrDefault());

            return resultState;
        }
    }
}