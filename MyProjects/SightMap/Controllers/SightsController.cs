using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System.Collections.Generic;

namespace SightMap.Controllers
{
    public class SightsController : BaseController<SightDTO, SightFilterDTO>
    {
        public SightsController(IBaseManager<SightDTO, SightFilterDTO> manager) : base(manager) { }
    }
}
