using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SightMap.BLL.DTO
{
    [MetadataType(typeof(SightTypeDTOMetadata))]
    public class SightTypeDTO : BaseDTO
    {
        public string Name { get; set; }
    }

    internal class SightTypeDTOMetadata
    {
        [FromQuery(Name = "SightTypeId")]
        public int Id { get; set; }
    }
}
