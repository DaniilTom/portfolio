using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.DTO
{
    public class SightDTO : ShortSightDTO
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string ShortDescription { get; set; }
        //public string PhotoPath { get; set; }
        public string FullDescription { get; set; }
        public string TypeName { get; set; }
        public int AuthorId { get; set; }
    }
}
