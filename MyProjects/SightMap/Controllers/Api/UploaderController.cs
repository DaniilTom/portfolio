using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SightMap.BLL.PluploadManager;

namespace SightMap.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {

        private IPluploadManager _uploadManager;

        public UploaderController(IPluploadManager uploadManager)
        {
            _uploadManager = uploadManager;
        }

        [HttpPost]
        public void Upload([FromForm] FileDataDTO dataDTO)
        {
            try
            {
                if (dataDTO.Chunks > 0)
                {
                    _uploadManager.SaveChunk(Request.Form.Files[0],
                                                dataDTO.RefId.ToString(),
                                                dataDTO.Name,
                                                dataDTO.Chunk,
                                                dataDTO.Chunks);
                }
                else
                {
                    _uploadManager.SaveFile(Request.Form.Files[0], dataDTO.RefId.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public string GetRefId()
        {
            return _uploadManager.GetReferenceId();
        }


        public class FileDataDTO
        {
            [FromForm(Name = "file[refId]")]
            public string RefId { get; set; }
            public int Chunk { get; set; }
            public int Chunks { get; set; }
            public string Name { get; set; }
        }
    }
}
