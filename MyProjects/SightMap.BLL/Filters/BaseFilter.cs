using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public abstract class BaseFilter<T> : IFilter<T> where T : BaseEntity
    {
        public int Id { get; }
        public int Offset { get; }
        public int Size { get; }

        protected BaseFilter(BaseFilterDTO filterDto)
        {
            Id = filterDto.Id;
            Offset = filterDto.Offset;
            Size = filterDto.Size;
        }

        /// <summary>
        /// Метод проверки объекта на соответствие заланным параметрам. 
        /// Параметр считается незаданным, если имеет значение по умолчанию (null или 0).
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool IsStatisfy(T item);
    }
}
