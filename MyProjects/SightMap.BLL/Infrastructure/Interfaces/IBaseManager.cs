using SightMap.BLL.DTO;
using System.Collections.Generic;

namespace SightMap.BLL.Infrastructure.Interfaces
{
    public interface IBaseManager<TFullDto, TFilterDto> 
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
        IEnumerable<TFullDto> GetListObjects(TFilterDto filterDto, bool IsCacheUsed = true);

        /// <summary>
        /// Метод получения дополнительной информации.
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        int? GetCount(TFilterDto filterDto);
    }
}
