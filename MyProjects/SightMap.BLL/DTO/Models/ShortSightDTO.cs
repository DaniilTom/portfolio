using System;

namespace SightMap.BLL.DTO
{
    public class ShortSightDTO : BaseDTO
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string PhotoPath { get; set; }
        public SightTypeDTO Type { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
