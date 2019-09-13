using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _Client;

        protected string ServiceAddress { get; }

        protected BaseClient(IConfiguration configuration, string ServiceAdress)
        {
            this.ServiceAddress = ServiceAdress;

            _Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };

            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Метод Post с поддержкой <see cref="CancellationToken"/>
        /// </summary>
        /// <param name="address"></param>
        /// <param name="user"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            return (await _Client.PostAsJsonAsync(url, item, Cancel)).EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Метод Put с поддержкой <see cref="CancellationToken"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="item"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            return (await _Client.PutAsJsonAsync(url, item, Cancel)).EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Метод Get с поддержкой <see cref="CancellationToken"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string url, CancellationToken Cancel = default) where T : new()
        {
            var response = await _Client.GetAsync(url, Cancel);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>(Cancel);
            return new T();
        }
    }
}
