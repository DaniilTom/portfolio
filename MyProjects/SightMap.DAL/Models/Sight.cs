using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Spatial;

namespace SightMap.DAL.Models
{
    [Table("Sights")]
    public class Sight : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ShortDescription { get; set; }

        [MaxLength]
        public string? FullDescription { get; set; }

        [MaxLength(250)]
        public string PhotoPath { get; set; }

        public int AuthorId { get; set; }

        public int SightTypeId { get; set; }

        [ForeignKey(nameof(SightTypeId))]
        public SightType Type { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Point Coordinates { get; set; }
        [NotMapped]
        public List<Album> Album { get; set; }
    }
}
