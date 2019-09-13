using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Common.Attributes
{
    /// <summary>
    /// Помечает List, значение которого могут быть приведены к типу int.
    /// Применяется при автоматизированном генерировании SQL-запроса.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DBListAttribute : Attribute
    {
        public DBListAttribute(string columnName, int maxCount)
        {
            ColumnName = columnName;
            MaxCount = maxCount;
        }

        public string ColumnName { get; }
        public int MaxCount { get; }
    }
}
