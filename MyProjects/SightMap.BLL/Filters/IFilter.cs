using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public interface IFilter<T> where T : Base
    {
        bool IsStatisfy(T itemToCheck);
        int Offset { get; set; }
        int Size { get; set; }
    }
}
