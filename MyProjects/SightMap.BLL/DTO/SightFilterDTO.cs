using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightFilterDTO : BaseFilterDTO
    {
        public string TypeName { get; set; }

        public SightFilterDTO() : base()
        {
            TypeName = "";
        }
    }
}
