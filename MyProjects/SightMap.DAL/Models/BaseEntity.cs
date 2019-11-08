using System.ComponentModel.DataAnnotations;

namespace SightMap.DAL.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
