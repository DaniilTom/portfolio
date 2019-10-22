using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SightMap.DAL.Models
{
    [Table("SightTypes")]
    public class SightType : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
