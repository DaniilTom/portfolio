﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightFilterDTO : BaseFilterDTO
    {
        public string Name { get; set; }
        public int SightTypeId { get; set; }
        public DateTime? CreateBeforeDate { get; set; }
        public DateTime? CreateAfterDate { get; set; }
        public DateTime? UpdateBeforeDate { get; set; }
        public DateTime? UpdateAfterDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + Name + SightTypeId + CreateBeforeDate + CreateAfterDate + 
                UpdateBeforeDate + UpdateAfterDate;
        }
    }
}
