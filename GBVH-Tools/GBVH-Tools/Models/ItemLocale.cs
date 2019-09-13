using GBVH_Tools.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBVH_Tools.Models
{
    /// <summary>
    /// Повторяет таблицу локалей
    /// </summary>
    public class ItemLocale
    {
        [DBPrimaryKey]
        public int Id { get; set; }
        /// <summary>
        /// Id предмета, к которому привязан текст
        /// </summary>
        [DBTargetKey]
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }
    }
}
