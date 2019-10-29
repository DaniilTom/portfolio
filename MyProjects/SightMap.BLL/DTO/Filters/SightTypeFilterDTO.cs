using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightTypeFilterDTO : BaseFilterDTO
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return base.ToString() + Name;
        }
    }
}
