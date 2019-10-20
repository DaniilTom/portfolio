using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public class SightTypeFilter : BaseFilter<SightType>
    {
        public SightTypeFilter(SightTypeFilterDTO dto) : base(dto) { }
    }
}
