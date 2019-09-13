using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Interfaces;

namespace WebStore.Domain.Implementations
{
    [Table("Categories")]
    public class Category : ICategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TotalProductsCount { get; set; }

        public string Name { get; set; }
    }
}
