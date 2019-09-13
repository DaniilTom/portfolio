using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebStore.Domain.Implementations
{
    [Table("Orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Contact { get; set; }

        public int TotalPrice { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } // отношение один Order ко многим OrderItem
    }

    [Table("OrderItems")]
    public class OrderItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // т.к. добавлено отношение в Order, колонка в OrderItem с указание на кортеж Order добавиться автоматически (OrderId)

        public virtual Order Order { get; set; }
        public virtual ProductBase Product { get; set; }

        //[ForeignKey(nameof(OrderId))]
        //public virtual Order Order { get; set; }

        //public int ProductId { get; set; }

        //[ForeignKey(nameof(ProductId))]
        //public virtual ProductBase Product { get; set; }

        public int Quantity { get; set; }
    }
}
