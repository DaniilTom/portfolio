using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Interfaces
{
    public interface IProductDescription
    {
        int ProductId { get; set; }
        string[] DetailedDesriptionList { get; set; }
    }
}
