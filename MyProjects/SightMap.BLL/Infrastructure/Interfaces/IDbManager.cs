using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using System.Collections.Generic;

namespace SightMap.BLL.Infrastructure.Interfaces
{
    public interface IDbManager<TFullDto, TShortDto, TFilterDto> 
        where TFullDto : TShortDto
        where TFilterDto : BaseFilterDTO
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
        IEnumerable<TShortDto> GetListObjects(TFilterDto filterDto);

        /// <summary>
        /// Метод получения целого объекта.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TFullDto GetObject(int id);

    }
}
