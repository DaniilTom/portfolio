namespace SightMap.BLL.DTO
{
    public class SightDTO : ShortSightDTO
    {
        public string FullDescription { get; set; }
        public int AuthorId { get; set; }
    }
}
