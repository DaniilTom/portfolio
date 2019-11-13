using System.ComponentModel.DataAnnotations.Schema;

namespace SightMap.DAL.Models
{
    [Table("Albums")]
    public class Album : BaseEntity
    {
        public int ItemId  { get; set; }
	    public string ImageName { get; set; }
        public string Title { get; set; }
	    public bool IsMain { get; set; }
    }
}
