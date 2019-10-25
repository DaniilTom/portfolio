using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System;
using System.Collections.Generic;

namespace SightMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TFullDto, TFilterDto> : Controller
        where TFullDto : BaseDTO
        where TFilterDto : BaseFilterDTO, new()
    {
        private IBaseManager<TFullDto, TFilterDto> _manager;
        private IMemoryCache _cache;

        protected BaseController(IBaseManager<TFullDto, TFilterDto> manager, IMemoryCache cache)
        {
            _manager = manager;
            _cache = cache;
        }

        [HttpPost]
        public ResultState<TFullDto> Post([FromBody] TFullDto dto)
        {
            var resultObject = _manager.Add(dto);
            var resultState = new ResultState<TFullDto>(resultObject);

            return resultState;
        }

        [HttpPut]
        public ResultState<TFullDto> Put(TFullDto dto)
        {
            var resultObject = _manager.Edit(dto);
            var resultState = new ResultState<TFullDto>(resultObject);

            return resultState;
        }

        [HttpDelete]
        public ResultState<TFullDto> Delete(int id)
        {
            var success = _manager.Delete(id);
            ResultState<TFullDto> resultState = new ResultState<TFullDto>(null);
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
        public ResultState<IEnumerable<TFullDto>> Get([FromQuery] TFilterDto filter)
        {
            var resultObject = _manager.GetListObjects(filter);
            var resultState = new ResultState<IEnumerable<TFullDto>>(resultObject);

            return resultState;
        }
    }
}
