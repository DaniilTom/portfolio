using SightMap.BLL.Filters;
using System.Collections.Generic;

namespace SightMap.BLL.Infrastructure.Interfaces
{
    public interface IDataAccess<TFullDto, TShortDto, TFilter> where TFullDto : TShortDto
                                                               where TFilter : BaseFilter
    {
        TFullDto Add(TFullDto dto);

        /// <summary>
        /// Метод редактирования объекта.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        TFullDto Edit(TFullDto dto);

        /// <summary>
        /// Метод удаления объекта.
        /// </summary>
        /// <param name="id"></param>
        bool Delete(int id);

        /// <summary>
        /// Метод постраничного получения кратких сведений об ообъектах.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TShortDto> GetListObjects(TFilter filter);

        /// <summary>
        /// Метод получения целого объекта.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TFullDto GetObject(int id);
    }
}
