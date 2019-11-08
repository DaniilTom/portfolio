using SightMap.DAL.Models;
using System.Collections.Generic;

namespace SightMap.BLL.DTO
{
    public class SightDTO : ShortSightDTO
    {
        public string FullDescription { get; set; }
        public int AuthorId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<AlbumDTO> Album { get; set; } = new List<AlbumDTO>();
    }
}
