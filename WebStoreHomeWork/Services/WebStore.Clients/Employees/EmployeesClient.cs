using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IServiceEmployeeData
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration, "api/EmployeesApi") { }

        public IEnumerable<Employee> Employees => GetAsync().Result;
        
        /*------------------------------------------------------*/
        public async Task<IEnumerable<Employee>> GetAsync()
        {
            var response = await _Client.GetAsync(ServiceAddress);
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<Employee>>().Result;
            return new List<Employee>();
        }

        public void AddNew(Employee employee) => PutAsync(employee);

        /*------------------------------------------------------*/
        public async Task<HttpResponseMessage> PutAsync(Employee employee)
        {
            var response = await _Client.PutAsJsonAsync<Employee>(ServiceAddress, employee);
            return response.EnsureSuccessStatusCode();
        }

        /*------------------------------------------------------*/
        public void Delete(int id) => DeleteAsync(id);
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var response = await _Client.DeleteAsync(ServiceAddress + "/" + id);
            return response.EnsureSuccessStatusCode();
        }

        /*------------------------------------------------------*/
        public void Edit(Employee employee) => EditAsync(employee);
        public async Task<HttpResponseMessage> EditAsync(Employee employee)
        {
            var response = await _Client.PostAsJsonAsync<Employee>(ServiceAddress, employee);
            return response.EnsureSuccessStatusCode();
        }

        /*------------------------------------------------------*/
        // в контроллере UI всегда запрашивается весь список Employees, а уже из него выбирается нужный
        // поэтому этот метод оставлю без реализации
        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        /*------------------------------------------------------*/
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
