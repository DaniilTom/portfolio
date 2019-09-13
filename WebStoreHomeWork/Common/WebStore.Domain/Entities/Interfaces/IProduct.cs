using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        int CategoryId { get; set; }
        string ImageUrl { get; set; }
        int Price { get; set; }
    }
}
