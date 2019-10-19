namespace SightMap.BLL.Filters
{
    public class BaseFilter
    {
        public BaseFilter()
        {
            Offset = 0;
            Size = int.MaxValue;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
    }
}
