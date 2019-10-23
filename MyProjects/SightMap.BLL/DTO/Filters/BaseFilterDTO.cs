using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public abstract class BaseFilterDTO
    {
        public BaseFilterDTO()
        {
            Offset = 0;
            Size = int.MaxValue;
        }

        public int Id { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
    }
}
