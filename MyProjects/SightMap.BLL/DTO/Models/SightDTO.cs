using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightDTO : ShortSightDTO
    {
        public int SightTypeId { get; set; }
        public string FullDescription { get; set; }
        public int AuthorId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
