using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using System.Collections.Generic;

namespace SightMap.BLL.Infrastructure.Interfaces
{
    public interface IDbManager<TFullDto, TFilterDto> 
        where TFullDto : BaseDTO
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
        /// Метод постраничного получения сведений об ообъектах.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TFullDto> GetListObjects(TFilterDto filterDto);
    }
}
