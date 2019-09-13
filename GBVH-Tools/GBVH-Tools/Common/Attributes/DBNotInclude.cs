using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Common.Attributes
{
    /// <summary>
    /// Помечает свойство, которое не должно учитываться при составлении SQL-запроса.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DBNotInclude : Attribute { }
}
