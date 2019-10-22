using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public class SightTypeFilter : BaseFilter<SightType>
    {
        public string Name { get; set; }

        public SightTypeFilter(SightTypeFilterDTO dto) : base(dto)
        {
            Name = string.Empty;
        }

    }
}
