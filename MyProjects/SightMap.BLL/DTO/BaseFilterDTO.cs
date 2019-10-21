using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class BaseFilterDTO
    {
        public BaseFilterDTO()
        {
            Offset = 0;
            Size = int.MaxValue;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
    }
}
