using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.DTO
{
    public class CreateOrderModel
    {
        public OrderDTO Order { get; set; }

        public List<OrderItemDTO> OrderItemsDTO { get; set; } = new List<OrderItemDTO>();
    }
}
