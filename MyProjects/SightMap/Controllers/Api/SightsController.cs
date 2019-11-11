using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System.IO;
using System;
using System.Collections.Generic;

namespace SightMap.Controllers.Api
{
    public class SightsController : BaseApiController<SightDTO, SightFilterDTO>
    {
        public SightsController(IBaseManager<SightDTO, SightFilterDTO> manager, IHostEnvironment host) : base(manager, host) { }
    }
}
