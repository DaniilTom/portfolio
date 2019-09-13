using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Common.Attributes
{
    /// <summary>
    /// Помечает Dictionary, ключ и значение которого могут быть приведены к типу int.
    /// Применяется при автоматизированном генерировании SQL-запроса.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DBDictionaryAttribute : Attribute
    {
        public DBDictionaryAttribute(string keyColumn, string valueColumn, int maxCount)
        {
            this.KeyColumn = keyColumn;
            this.ValueColumn = valueColumn;
            this.MaxCount = maxCount;
        }

        public string KeyColumn { get; }
        public string ValueColumn { get; }
        public int MaxCount { get; }
    }
}
