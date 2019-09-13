using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;
using WebStore.Interfaces.Services;

namespace WebStore.Infrastructure.Implementations
{
    public class SqlProductData : IServiceAllData
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductDTO> Products
        {
            get
            {
                List<ProductDTO> ProductsDTO = new List<ProductDTO>();
                var products = _db.Products.Include(o => o.Category).AsEnumerable();
                foreach (var product in products)
                {
                    ProductsDTO.Add(new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CategoryId = product.CategoryId,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        //Category = product.Category
                    });
                }
                return ProductsDTO;
            }
        }

        public IEnumerable<MCDescriptionDTO> DetailedDescription
        {
            get
            {
                List<MCDescriptionDTO> DescDTO = new List<MCDescriptionDTO>();
                var descriptions = _db.MCDescriptions.Include(d => d.Product);
                return descriptions.Select(d => new MCDescriptionDTO
                {
                    Id = d.Id,
                    ProductId = d.Product.Id,
                    ProductName = d.Product.Name,
                    DetailedDesription = String.Join(";", d.DetailedDesriptionList)
                });
            }
        }

        public IEnumerable<Category> Categories => _db.Categories;

        public IEnumerable<OrderDTO> Orders
        {
            get
            {
                List<OrderDTO> OrdersDTO = new List<OrderDTO>();
                var orders = _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).AsEnumerable();
                foreach(var order in orders)
                {
                    OrdersDTO.Add(new OrderDTO
                    {
                        Id = order.Id,
                        UserName = order.UserName,
                        Contact = order.Contact,
                        TotalPrice = order.TotalPrice,
                        Items = order.Items.Select(i => new OrderItemDTO
                        {
                            Id = i.Id,
                            ProductId = i.Product.Id,
                            ProductName = i.Product.Name,
                            OrderId = i.Order.Id,
                            Quantity = i.Quantity
                        })
                    });
                }
                return OrdersDTO;
            }
        }

        public IEnumerable<OrderItemDTO> OrderItems
        {
            get
            {
                var o_items = _db.OrderItems;
                return o_items.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    ProductId = i.Product.Id,
                    OrderId = i.Order.Id,
                    Quantity = i.Quantity
                });
            }
        }

        public void AddNewProduct(ProductDTO prod)
        {
            _db.Products.Add(new ProductBase
            {
                Id = prod.Id,
                CategoryId = prod.CategoryId,
                Name = prod.Name,
                ImageUrl = prod.ImageUrl,
                Price = prod.Price,
                Category = _db.Categories.FirstOrDefault(c => c.Id == prod.Id)
            });
            _db.SaveChanges();
        }

        public void AddNewDescription(MCDescriptionDTO description)
        {
            _db.MCDescriptions.Add(new MCDescription
            {
                Id = description.Id,
                ProductId = description.ProductId,
                Product = _db.Products.FirstOrDefault(p => p.Id == description.ProductId),
                DetailedDesriptionList = description.DetailedDesription.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            });
            _db.SaveChanges();
        }

        public void AddNewOrder(CreateOrderModel Model)
        {
            var order = new Order
            {
                UserName = Model.Order.UserName,
                Contact = Model.Order.Contact,
                TotalPrice = Model.Order.TotalPrice
            };

            _db.Orders.Add(order);

            foreach (var item in Model.OrderItemsDTO)
            {
                var order_item = new OrderItem
                {
                    Order = order,
                    Product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId),
                    Quantity = item.Quantity
                };
                _db.OrderItems.Add(order_item);
            }

            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var prod = _db.Products.FirstOrDefault(p => p.Id == id);
            _db.Products.Remove(prod);
            _db.SaveChanges();
        }

        public ProductBase GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public Order GetOrderById(int Id)
        {
            return _db.Orders.FirstOrDefault(o => o.Id == Id);
        }

        
    }
}
