using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System.Collections.Generic;
using System.IO;

namespace SightMap.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController<TFullDto, TFilterDto> : Controller
        where TFullDto : BaseDTO
        where TFilterDto : BaseFilterDTO, new()
    {
        protected IBaseManager<TFullDto, TFilterDto> _manager;
        protected IHostEnvironment _host;

        protected BaseApiController(IBaseManager<TFullDto, TFilterDto> manager, IHostEnvironment host)
        {
            _manager = manager;
            _host = host;
        }

        [HttpPost(Order = int.MaxValue)]
        public virtual ResultState<TFullDto> Post([FromForm] TFullDto dto)
        {
            var resultObject = _manager.Add(dto);
            var resultState = new ResultState<TFullDto>(resultObject);

            return resultState;
        }

        [HttpPost("{id}", Order = int.MaxValue)]
        public virtual ResultState<TFullDto> PostEdit([FromForm]TFullDto dto)
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
            {
                resultState.IsSuccess = true;
                resultState.Message = "";
            }
            else
            {
                resultState.IsSuccess = false;
                resultState.Message = Constants.ErrorIdWrong;
            }

            return resultState;
        }

        [HttpGet]
        public virtual ResultState<IEnumerable<TFullDto>> Get([FromQuery]TFilterDto filter)
        {
            var resultObject = _manager.GetListObjects(filter);
            var resultState = new ResultState<IEnumerable<TFullDto>>(resultObject);

            return resultState;
        }
    }
}
