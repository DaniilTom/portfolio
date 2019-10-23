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
    public class SightsController : BaseController<SightDTO, SightFilterDTO>
    {
        public SightsController(IBaseManager<SightDTO, SightFilterDTO> manager) : base(manager) { }
    }
}
