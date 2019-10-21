using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public class SightFilter : BaseFilter<Sight>
    {
        public int SightTypeId { get; }

        public SightFilter(SightFilterDTO dto) : base(dto)
        {
            SightTypeId = dto.SightTypeId;
        }

        public override bool IsStatisfy(Sight obj)
        {
            return (obj.SightTypeId == SightTypeId) || base.IsStatisfy(obj);
        }
    }
}
