using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeesApiController : ControllerBase // без интерфейса обошлось
    {
        private readonly IServiceEmployeeData _EmployeeData;

        public EmployeesApiController(IServiceEmployeeData EmployeeData)
        {
            _EmployeeData = EmployeeData;
        }

        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _EmployeeData.Employees;
        }

        [HttpPut]
        public void PutNew([FromBody] Employee employee)
        {
            _EmployeeData.AddNew(employee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _EmployeeData.Delete(id);
        }

        [HttpPost]
        public void Edit([FromBody] Employee employee)
        {
            _EmployeeData.Edit(employee);
        }
    }
}