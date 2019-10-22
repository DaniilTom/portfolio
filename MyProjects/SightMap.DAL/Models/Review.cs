using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SightMap.DAL.Models
{
    [Table("Reviews")]
    public class Review : BaseEntity
    {
        public int ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Review Parent { get; set; }

        public int ItemId { get; set; }

        public string Message { get; set; }

        public int AuthorId { get; set; }
    }
}
