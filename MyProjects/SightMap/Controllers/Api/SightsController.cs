using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.Models;
using System.IO;

namespace SightMap.Controllers.Api
{
    public class SightsController : BaseApiController<SightDTO, SightFilterDTO>
    {
        public SightsController(IBaseManager<SightDTO, SightFilterDTO> manager, IHostEnvironment host) : base(manager,host) { }

        [HttpPost(Order = 1)]
        public ResultState<SightDTO> Post([FromForm] SightDTO dto, IFormFile image)
        {
            if(image != null)
                dto.PhotoPath = "\\img\\" + image.FileName;
            else
                dto.PhotoPath = "\\img\\empty.jpg";

            var resultState = base.Post(dto);

            if (resultState.IsSuccess && (image != null))
            {
                LoadImage(image);
            }

            return resultState;
        }

        [HttpPost("{id}", Order = 1)]
        public ResultState<SightDTO> PostEdit([FromForm] SightDTO dto, IFormFile image)
        {
            if (image != null)
                dto.PhotoPath = "\\img\\" + image.FileName;
            else
                dto.PhotoPath = "\\img\\empty.jpg";

            var resultState = base.PostEdit(dto);

            if (resultState.IsSuccess && (image != null))
            {
                LoadImage(image);
            }

            return resultState;
        }



        protected async void LoadImage(IFormFile image)
        {
            string path = _host.ContentRootPath + "\\wwwroot\\img\\" + image.FileName;
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await image.CopyToAsync(fileStream);
            }
        }
    }
}
