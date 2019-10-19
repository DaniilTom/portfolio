using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;

namespace SightMap.BLL.Mappers
{
    /// <summary>
    /// Класс, содержащий методы отображения объектов <see cref="SightType"/> в объекты DTO.
    /// </summary>
    public static class SightTypeMapper
    {
        public static SightType ToSightType(this SightTypeDTO dto)
        {
            return new SightType
            {
                Name = dto.Name
            };
        }

        public static SightTypeDTO ToSightTypeDTO(this SightType type)
        {
            return new SightTypeDTO
            {
                Id = type.Id,
                Name = type.Name
            };
        }
    }
}
