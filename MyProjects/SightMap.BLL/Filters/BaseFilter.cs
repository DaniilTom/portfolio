using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public abstract class BaseFilter<T> : IFilter<T> where T : Base
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

        public virtual bool IsStatisfy(T obj)
        {
            return (obj.Id == Id) || (obj.Name.Equals(Name, System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
