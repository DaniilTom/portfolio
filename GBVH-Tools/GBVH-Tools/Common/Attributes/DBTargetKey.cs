using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Common.Attributes
{
    /// <summary>
    /// Помечает свойство, соответствующее столбцу, по которому будет идентифицироваться кортеж в БД.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DBTargetKey : Attribute { }
}
