using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Interfaces
{
    interface ICategory
    {
        string Name { get; set; }
        int Id { get; set; }
        int TotalProductsCount { get; set; }
    }
}
