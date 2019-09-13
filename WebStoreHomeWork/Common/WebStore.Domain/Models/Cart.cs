using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;
using WebStore.Domain.Interfaces;

namespace WebStore.Models
{
    public class ProductContainer
    {
        //public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int Count { get; set; }
    }

    public class Cart
    {
        public List<ProductContainer> Items { get; set; } = new List<ProductContainer>();

        //public int ItemsCount => Items?.Sum(item => item.Count) ?? 0;
    }
}
