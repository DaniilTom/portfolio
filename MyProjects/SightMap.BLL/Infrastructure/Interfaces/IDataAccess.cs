using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using System.Collections.Generic;

namespace SightMap.BLL.Infrastructure.Interfaces
{
    public interface IDataAccess<TFullDto, TShortDto, Source> where TFullDto : TShortDto
                                                              where Source : Base
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
        IEnumerable<TShortDto> GetListObjects(IFilter<Source> filter);

        /// <summary>
        /// Метод получения целого объекта.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TFullDto GetObject(int id);

    }
}
