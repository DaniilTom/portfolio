using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightFilterDTO : BaseFilterDTO
    {
        public string Name { get; set; }
        public int SightTypeId { get; set; }
        public DateTime? CreateUpDate { get; }
        public DateTime? CreateDownDate { get; }
        public DateTime? UpdateUpDate { get; }
        public DateTime? UpdateDownDate { get; }
        public bool FilterByCreateDate { get; set; }
        public bool FilterByUpdateDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + Name + SightTypeId + CreateUpDate + CreateDownDate + 
                UpdateUpDate + UpdateDownDate + FilterByCreateDate + FilterByUpdateDate;
        }
    }
}
