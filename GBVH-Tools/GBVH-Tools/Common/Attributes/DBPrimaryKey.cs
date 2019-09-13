using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Common.Attributes
{
    /// <summary>
    /// Помечает свойство, содержащее в себсе знаение перивчного ключа. Свойство будет
    /// игнорироваться в части SET при построении запросов UPDATE. Для указания ключа, по которому будет
    /// осуществляться поиск строки БД, используйте <see cref="DBTargetKey="/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DBPrimaryKey : Attribute
    {
        public DBPrimaryKey() { }
    }
}
