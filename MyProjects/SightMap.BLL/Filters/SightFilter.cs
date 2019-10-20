using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public class SightFilter : BaseFilter<Sight>
    {
        public string TypeName { get; set; }

        public override bool IsStatisfy(Sight obj)
        {
            return base.IsStatisfy(obj) || (obj.Type.Name.Equals(TypeName, System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
