using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;
using WebStore.Domain.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.controllers
{
    public class CartController : Controller
    {
        private readonly IServiceCart _CartService;
        private readonly IServiceAllData _DataBase;

        public CartController(IServiceCart CartService, IServiceAllData DataBase)
        {
            _CartService = CartService;
            _DataBase = DataBase;
        }

        public IActionResult Cart()
        {
            string _CartName = $"{User.Identity.Name}_cart";
            var cookie = HttpContext.Request.Cookies[_CartName];

            if (cookie is null)
                return View(new Cart());

            return View(JsonConvert.DeserializeObject<Cart>(cookie));
        }

        public IActionResult AddToCart(int id)
        {
            IProduct product = _DataBase.Products.FirstOrDefault(m => m.Id == id);
            _CartService.AddToCart(product);
            return RedirectToAction("Cart");
        }

        public IActionResult AddToCartAJAX(int id)
        {
            AddToCart(id);
            return Json(new { id, message = $"Товар {id} добавлен" });
        }

        public IActionResult DecrementProduct(int id)
        {
            IProduct product = _DataBase.Products.FirstOrDefault(m => m.Id == id);
            _CartService.DecrementProduct(product);
            return RedirectToAction("Cart");
        }
        public IActionResult DecrementFromCartAJAX(int id)
        {
            DecrementProduct(id);
            return Json(new { id, message = $"Товар {id} убавлен" });
        }

        public IActionResult RemoveFromCart(int id)
        {
            IProduct product = _DataBase.Products.FirstOrDefault(m => m.Id == id);
            _CartService.RemoveFromCart(product);
            return RedirectToAction("Cart");
        }
        public IActionResult RemoveFromCartAJAX(int id)
        {
            RemoveFromCart(id);
            return Json(new { id, message = $"Товар {id} удален" });
        }

        public IActionResult Checkout()
        {
            string _CartName = $"{User.Identity.Name}_cart";
            var cookie = HttpContext.Request.Cookies[_CartName];
            Cart cart = JsonConvert.DeserializeObject<Cart>(cookie);

            CreateOrderModel model = new CreateOrderModel();

            var order = new OrderDTO
            {
                UserName = User.Identity.Name,
                Contact = "1 5 Net Core st.",
                TotalPrice = cart.Items.Sum(pc => pc.Count * pc.Product.Price)
            };

            //_DataBase.AddNewOrder(order);
            model.Order = order;

            foreach(var item in cart.Items)
            {
                var order_item = new OrderItemDTO
                {
                    ProductId = item.Product.Id,
                    OrderId = order.Id, 
                    Quantity = item.Count
                };
                //_DataBase.AddNewOrderItem(order_item);
                model.OrderItemsDTO.Add(order_item);
            }

            _DataBase.AddNewOrder(model);

            HttpContext.Response.Cookies.Delete(_CartName);

            return RedirectToAction("Catalog", "Home");
        }
    }
}