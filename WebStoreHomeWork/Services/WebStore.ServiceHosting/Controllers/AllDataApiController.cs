using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AllDataApiController : ControllerBase, IServiceAllData
    {
        private readonly IServiceAllData _DataBase;

        public AllDataApiController(IServiceAllData db)
        {
            _DataBase = db;
        }

        [HttpGet("Orders")]
        public IEnumerable<OrderDTO> GetOrders() // нужен метод для аттрибутов
        {
            return Orders;
        }
        public IEnumerable<OrderDTO> Orders => _DataBase.Orders;

        [HttpGet("OrderItems")]
        public IEnumerable<OrderItemDTO> GetOrderItems()
        {
            return OrderItems;
        }
        public IEnumerable<OrderItemDTO> OrderItems => _DataBase.OrderItems;

        [HttpGet("Products")]
        public IEnumerable<ProductDTO> GetProducts()
        {
            return Products;
        }
        public IEnumerable<ProductDTO> Products => _DataBase.Products;

        [HttpGet("Categories")]
        public IEnumerable<Category> GetCategories()
        {
            return Categories;
        }
        public IEnumerable<Category> Categories => _DataBase.Categories;

        [HttpGet("Descriptions")]
        public IEnumerable<MCDescriptionDTO> GetDescriptions()
        {
            return DetailedDescription;
        }
        public IEnumerable<MCDescriptionDTO> DetailedDescription => _DataBase.DetailedDescription;

        [HttpPut("new/Description")]
        public void AddNewDescription([FromBody] MCDescriptionDTO description)
        {
            _DataBase.AddNewDescription(description);
        }

        [HttpPut("new/Order")]
        public void AddNewOrder([FromBody] CreateOrderModel order)
        {
            _DataBase.AddNewOrder(order);
        }
        
        //[HttpPut("new/OrderItem")]
        //public void AddNewOrderItem([FromBody] OrderItemDTO orderItem)
        //{
        //    _DataBase.AddNewOrderItem(orderItem);
        //}
        
        [HttpPut("new/Product")]
        public void AddNewProduct([FromBody] ProductDTO product)
        {
            _DataBase.AddNewProduct(product);
        }

        public void DeleteProduct(int id)
        {
            _DataBase.DeleteProduct(id);
        }

        public Order GetOrderById(int Id)
        {
            return _DataBase.GetOrderById(Id);
        }

        public ProductBase GetProductById(int Id)
        {
            return _DataBase.GetProductById(Id);
        }

        public void AddNewOrder(OrderDTO order)
        {
            throw new NotImplementedException();
        }
    }
}