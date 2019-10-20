using Microsoft.AspNetCore.Mvc;
using SightMap.Attributes;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.DAL.Models;
using SightMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightTypesController : Controller
    {
        private IDataAccess<SightTypeDTO, SightTypeDTO, SightType> dataStore;
        public SightTypesController(IDataAccess<SightTypeDTO, SightTypeDTO, SightType> _dataStore)
        {
            dataStore = _dataStore;
        }

        [HttpPost]
        public ResultState<SightTypeDTO> Post([FromBody] SightTypeDTO dto)
        {
            var resultObject = dataStore.Add(dto);
            var resultState = ResultState<SightTypeDTO>.CreateResulState<SightTypeDTO>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<SightTypeDTO> Put(SightTypeDTO dto)
        {
            var resultObject = dataStore.Edit(dto);
            var resultState = ResultState<SightTypeDTO>.CreateResulState<SightTypeDTO>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<SightTypeDTO> Delete(int id)
        {
            var success = dataStore.Delete(id);
            ResultState<SightTypeDTO> resultState = new ResultState<SightTypeDTO>();
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
        public ResultState<IEnumerable<SightTypeDTO>> Get([FromQuery] SightTypeFilter filter)
        {
            var resultObject = dataStore.GetListObjects(filter);
            var resultState = ResultState<IEnumerable<SightTypeDTO>>.CreateResulState<IEnumerable<SightTypeDTO>>(resultObject);

            return resultState;
        }

        [HttpGet]
        public ResultState<SightTypeDTO> Get([RequiredFromQuery]int id)
        {
            var resultObject = dataStore.GetObject(id);
            var resultState = ResultState<SightTypeDTO>.CreateResulState<SightTypeDTO>(resultObject);
            return resultState;
        }
    }
}