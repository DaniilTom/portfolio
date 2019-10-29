﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class ReviewFilterDTO : BaseFilterDTO
    {
        public int ParentId { get; set; }
        public int ItemId { get; set; }

        public override string ToString()
        {
            return base.ToString() + ParentId + ItemId;
        }
    }
}
