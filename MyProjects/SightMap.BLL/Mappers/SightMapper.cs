﻿using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.Mappers
{
    /// <summary>
    /// Класс, содержащий методы отображения объектов <see cref="Sight"/> в объекты DTO.
    /// </summary>
    public static class SightMapper
    {
        /// <summary>
        /// Метод отображения <see cref="Sight"/> в <see cref="SightDTO"/>.
        /// </summary>
        /// <param name="sight"></param>
        /// <returns></returns>
        public static SightDTO ToSightDTO(this Sight sight)
        {
            return new SightDTO
            {
                Id = sight.Id,
                Name = sight.Name,
                TypeName = sight.Type.Name,
                ShortDescription = sight.ShortDescription,
                FullDescription = sight.FullDescription,
                PhotoPath = sight.PhotoPath,
                AuthorId = sight.AuthorId
            };
        }

        /// <summary>
        /// Метод отображения <see cref="Sight"/> в сокращенную <see cref="ShortSightDTO"/>.
        /// </summary>
        /// <param name="sight"></param>
        /// <returns></returns>
        public static ShortSightDTO ToShortSightDTO(this Sight sight)
        {
            return new ShortSightDTO
            {
                Id = sight.Id,
                Name = sight.Name,
                ShortDescription = sight.ShortDescription,
                PhotoPath = sight.PhotoPath
            };
        }

        /// <summary>
        /// Метод отображения DTO в <see cref="Sight"/>. Привязка <see cref="Sight.Type"/> выполняется отдельно.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Sight ToSight(this SightDTO dto)
        {
            return new Sight
            {
                Name = dto.Name,
                FullDescription = dto.FullDescription,
                ShortDescription = dto.ShortDescription,
                PhotoPath = dto.PhotoPath,
                AuthorId = dto.AuthorId
            };
        }
    }
}
