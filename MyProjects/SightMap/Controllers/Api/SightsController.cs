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

        [HttpPost(Order = 1)]
        public ResultState<SightDTO> Post([FromForm] SightDTO dto, [FromForm] List<FileDTO> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i].File == null)
                    images.RemoveAt(i--);
                else
                    dto.Album.Add(new AlbumDTO {
                        IsMain = images[i].IsMain,
                        ImageName = images[i].File.FileName });
            }

            var resultState = base.Post(dto);

            if ((images.Count > 0) && resultState.IsSuccess)
            {
                var album = resultState.Value.Album;
                for (int i = 0; i < album.Count; i++)
                {
                    var image = images.Find(img => img.IsMain == album[i].IsMain);
                    LoadImage(image.File, album[i].ImageName);
                    images.Remove(image);
                }
            }

            return resultState;
        }

        [HttpPost("{id}", Order = 1)]
        public ResultState<SightDTO> PostEdit([FromForm] SightDTO dto, IFormFile image)
        {
            string fileName = "";
            if (image == null)
                dto.PhotoPath = "\\img\\empty.jpg";

            var resultState = base.PostEdit(dto);

            if (resultState.IsSuccess && (image != null))
            {
                fileName = resultState.Value.PhotoPath.Substring(resultState.Value.PhotoPath.LastIndexOf('\\'));
                LoadImage(image, fileName);
            }

            return resultState;
        }


        protected async void LoadImage(IFormFile image, string fileName)
        {
            string path = _host.ContentRootPath + "\\wwwroot\\" + fileName;
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                await image.CopyToAsync(fileStream);
            }
        }

        [Route("/geta")]
        public A GetA()
        {
            A a = new B();
            return a;
        }

        public class A
        {
            public int num { get; set; }
            public string str { get; set; }
            public A()
            {
                num = 99;
                str = "abc";
            }
        }

        public class B : A
        {
            public int numnum { get; set; }
            public string strstr { get; set; }

            public B()
            {
                numnum = 9999999;
                strstr = "dfghgdsf";
            }
        }
    }
}
