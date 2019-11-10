using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap.Models
{
    public class FileDTO
    {
        public IFormFile File { get; set; }
        public bool IsMain { get; set; }
    }
}
