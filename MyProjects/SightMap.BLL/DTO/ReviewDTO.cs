using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class ReviewDTO : BaseDTO
    {
        public int ParentId { get; set; }
        public int ItemId { get; set; }
        public string Message { get; set; }
        public int AuthorId { get; set; }
        public List<ReviewDTO> Children { get; set; } = new List<ReviewDTO>();
        public ReviewDTO Parent { get; set; }
    }
}
