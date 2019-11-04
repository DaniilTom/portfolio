namespace SightMap.BLL.DTO
{
    public abstract class BaseFilterDTO
    {
        public BaseFilterDTO()
        {
            Offset = 1;
            Size = int.MaxValue;
        }
        public int Id { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }

        public override string ToString()
        {
            return Id.ToString() + Offset + Size;
        }
    }
}
