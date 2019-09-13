using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Implementations;

namespace WebStore.Domain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Contact { get; set; }

        public int TotalPrice { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
