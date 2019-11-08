namespace SightMap.BLL.DTO
{
    public class AlbumFilterDTO : BaseFilterDTO
    {
        public int ItemId { get; set; }
        public bool IsMain { get; set; }

        public override string ToString()
        {
            return base.ToString() + ItemId + IsMain;
        }
    }
}
