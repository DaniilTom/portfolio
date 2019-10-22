using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public abstract class BaseFilter<T> : IFilter<T> where T : BaseEntity
    {
        protected BaseFilterDTO filterData;
        protected BaseFilter(BaseFilterDTO _filterData) { filterData = _filterData; }

        public int Offset { get => filterData.Offset; }
        public int Size { get => filterData.Size; }

        public virtual bool IsStatisfy(T item)
        {
            return (item.Id == filterData.Id); // || 
                //(item.Name.Equals(filterData.Name, System.StringComparison.CurrentCultureIgnoreCase)); // TODO: Filter Bug
        }
    }
}
