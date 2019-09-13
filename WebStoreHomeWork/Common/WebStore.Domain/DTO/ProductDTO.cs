using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Implementations;
using WebStore.Domain.Interfaces;

namespace WebStore.Domain.DTO
{
    public class ProductDTO : IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        //public Category Category { get; set; }

        public int Price { get; set; }
    }
}
