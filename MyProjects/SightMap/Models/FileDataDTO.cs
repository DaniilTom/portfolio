using Microsoft.AspNetCore.Mvc;

namespace SightMap.Models
{
    public partial class UploaderController
    {
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
