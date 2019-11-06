namespace SightMap.BLL.DTO
{
    public class SightDTO : ShortSightDTO
    {
        public string FullDescription { get; set; }
        public int AuthorId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
