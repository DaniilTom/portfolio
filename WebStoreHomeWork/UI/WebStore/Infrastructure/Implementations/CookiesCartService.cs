using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;
using WebStore.Domain.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.Infrastructure.Implementations
{
    public class CookiesCartService : IServiceCart
    {
        private readonly IServiceAllData _dataBase;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly string _CartName;

        public CookiesCartService(IServiceAllData serviceAllData, IHttpContextAccessor http)
        {
            _dataBase = serviceAllData;
            _httpAccessor = http;
            _CartName = $"{http.HttpContext.User.Identity.Name}_cart";
        }

        private Cart Cart
        {
            get
            {
                var cookie = _httpAccessor.HttpContext.Request.Cookies[_CartName];

                Cart cart = null;
                if(!String.IsNullOrEmpty(cookie))
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);
                    _httpAccessor.HttpContext.Response.Cookies.Delete(_CartName);
                    _httpAccessor.HttpContext.Response.Cookies.Append(_CartName, cookie, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
                }
                else
                {
                    cart = new Cart();
                    _httpAccessor.HttpContext.Response.Cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                }

                return cart;
            }

            set
            {
                _httpAccessor.HttpContext.Response.Cookies.Delete(_CartName);
                _httpAccessor.HttpContext.Response.Cookies.Append(
                    _CartName, 
                    JsonConvert.SerializeObject(value),
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
            }
        }

        public void AddToCart(IProduct product)
        {
            var cart = Cart;
            var _product = cart.Items.Find(pc => pc.Product.Id == product.Id);
            if(_product is null)
            {
                cart.Items.Add(new ProductContainer
                    {
                        Product = new ProductDTO
                        {
                            Id = product.Id,
                            CategoryId = product.CategoryId,
                            Name = product.Name,
                            ImageUrl = product.ImageUrl,
                            Price = product.Price
                        },
                        Count = 1
                    });
            }
            else
            {
                _product.Count++;
            }
            Cart = cart;
        }

        public void DecrementProduct(IProduct product)
        {
            var cart = Cart;
            var product_container = cart.Items.Find(pc => pc.Product.Id == product.Id);
            if (product_container.Count == 1)
                cart.Items.Remove(product_container);
            else
                product_container.Count--;
            Cart = cart;
        }

        public void IncrementProduct(IProduct product)
        {
            var cart = Cart;
            var product_container = cart.Items.Find(pc => pc.Product.Id == product.Id);

            product_container.Count++;
            Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void RemoveFromCart(IProduct product)
        {
            var cart = Cart;
            cart.Items.Remove(cart.Items.FirstOrDefault(pc => pc.Product.Id == product.Id));
            Cart = cart;
        }
    }
}
