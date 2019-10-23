using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightFilterDTO : BaseFilterDTO
    {
        public int SightTypeId { get; set; }
        public DateTime UpDate { get; set; }
        public DateTime DownDate { get; set; }
        public bool FilterByCreateDate { get; set; }
        public bool FilterByUpdateDate { get; set; }
    }
}
