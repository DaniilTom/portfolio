using SightMap.BLL.DTO;
using System.Collections.Generic;

namespace SightMap.BLL.Infrastructure.Interfaces
{
    public interface IAlbumEditor<TFullDto, TFilterDto> : IBaseManager<TFullDto, TFilterDto>
        where TFullDto : BaseDTO
        where TFilterDto : BaseFilterDTO
    {
        IEnumerable<TFullDto> Edit(IEnumerable<TFullDto> album, string refId);
    }
}
