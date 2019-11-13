using Microsoft.AspNetCore.Http;
using SightMap.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SightMap.BLL.DTO
{
    public class SightDTO : ShortSightDTO
    {
        public string FullDescription { get; set; }
        public int AuthorId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public IEnumerable<AlbumDTO> Album { get; set; }
        public string RefId { get; set; }
    }
}
