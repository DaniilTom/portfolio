using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class AlbumDTO : BaseDTO
    {
        public int ItemId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public State State { get; set; }
        public bool IsMain { get; set; }
    }

    public enum State
    {
        Nothing = 0,
        Add = 1,
        Edit = 2,
        Delete = 3
    }
}
