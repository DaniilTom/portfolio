using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValueService
    {
        public ValuesClient(IConfiguration Configuration)
            : base(Configuration, "api/values") { }

        public async Task<string> GetOk()
        {
            var response = await _Client.GetAsync(ServiceAddress + "/getok");

            if (response.IsSuccessStatusCode)
            {
                //return await response.Content.ReadAsAsync<string>();
                return await response.Content.ReadAsStringAsync();
            }

            return "<script>alert('Not Ok');</script>";
        }

        public IEnumerable<string> Get()
        {
            return GetAsync().Result;
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            // запрос самого сервиса
            var response = await _Client.GetAsync(ServiceAddress);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<string>>();
            }

            return new string[0];

        }

        public string Get(int id)
        {
            return GetAsync(id).Result;
        }

        public async Task<string> GetAsync(int id)
        {
            var response = await _Client.GetAsync($"{ServiceAddress}/get/id");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<string>();
            return string.Empty;
        }

        public Uri Post(string value)
        {
            return PostAsync(value).Result;
        }

        public async Task<Uri> PostAsync(string value)
        {
            var response = await _Client.PostAsJsonAsync($"{ServiceAddress}/post", value); // post - название метода контроллера
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public HttpStatusCode Put(int id, string value)
        {
            return PutAsync(id, value).Result;
        }

        public async Task<HttpStatusCode> PutAsync(int id, string value)
        {
            var response = await _Client.PutAsJsonAsync($"{ServiceAddress}/put/{id}", value);
            response.EnsureSuccessStatusCode();
            return response.StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            return DeleteAsync(id).Result;
        }

        public async Task<HttpStatusCode> DeleteAsync(int id)
        {
            var response = await _Client.DeleteAsync($"{ServiceAddress}/delete/{id}");
            return response.StatusCode;
        }
    }
}
