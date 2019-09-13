using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.AllData
{
    public class AllDataClient : BaseClient, IServiceAllData
    {
        public AllDataClient(IConfiguration configuration) : base (configuration, "api/AllDataApi") { }

        public IEnumerable<OrderDTO> Orders => GetAsync<OrderDTO>("Orders").Result;

        public IEnumerable<OrderItemDTO> OrderItems => GetAsync<OrderItemDTO>("OrderItems").Result;

        public IEnumerable<ProductDTO> Products => GetAsync<ProductDTO>("Products").Result;

        public IEnumerable<MCDescriptionDTO> DetailedDescription => GetAsync<MCDescriptionDTO>("Descriptions").Result;

        public IEnumerable<Category> Categories => GetAsync<Category>("Categories").Result;

        /// <summary>
        /// Общий для всех методов получения данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        private async Task<IEnumerable<T>> GetAsync<T>(string address)
        {
            var response = await _Client.GetAsync($"{ServiceAddress}/{address}");
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<T>>().Result;
            return new List<T>();
        }

        public void AddNewDescription(MCDescriptionDTO description) => PutNew<MCDescriptionDTO>("Description", description);

        //public void AddNewOrder(OrderDTO order) => PutNew<OrderDTO>("Order", order);

        //public void AddNewOrderItem(OrderItemDTO orderItem) => PutNew<OrderItemDTO>("OrderItem", orderItem);

        public void AddNewProduct(ProductDTO product) => PutNew<ProductDTO>("Product", product);

        public void AddNewOrder(CreateOrderModel model) => PutNew<CreateOrderModel>("Order", model);

        private async void PutNew<T>(string address, T obj)
        {
            var response = await _Client.PutAsJsonAsync<T>($"{ServiceAddress}/new/{address}", obj);
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int Id)
        {
            throw new NotImplementedException();
        }

        public ProductBase GetProductById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
