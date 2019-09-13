using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Interfaces.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebStore.controllers
{
    public class EmployesController : Controller
    {
        IServiceEmployeeData _EmployeesData;

        public EmployesController(IServiceEmployeeData ed)
        {
            _EmployeesData = ed;
        }

        public IActionResult Index()
        {
            //return Content("Hello World!!");
            return View(_EmployeesData.Employees);
        }

        public IActionResult Details(int id)
        {
            var employe = _EmployeesData.Employees.FirstOrDefault(e => e.Id == id);
            if (employe == null) return NotFound();

            return View(employe);
        }

        public IActionResult Dossier(int id)
        {
            var employe = _EmployeesData.Employees.FirstOrDefault(e => e.Id == id);
            if (employe == null)
            {
                return NotFound();

                //хотел вместо NotFound() показывать alert() поверх текущего View (без перезагрузки страницы),
                // но способ снизу загружает новую страницу и исполняет JS
                //string str = "<script>alert(\"Не найдено\");</script>";
                //byte[] b = Encoding.UTF8.GetBytes(str);
                //HttpContext.Response.Body.WriteAsync(b);
            }

            return View(employe);
        }

        [Authorize(Roles = Domain.User.AdminRoleName)]
        public IActionResult Edit(int Id)
        {
            if (Id != 0) return View(_EmployeesData.Employees.First(emp => emp.Id == Id));
            else return View();
        }

        [Authorize(Roles = Domain.User.AdminRoleName)]
        public IActionResult Delete(int id)
        {
            _EmployeesData.Delete(id);
            return Redirect("/Employes/Index");
        }

        [HttpPost]
        [Authorize(Roles = Domain.User.AdminRoleName)]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid && employee.Id != 0)
                return View(employee);

            if (employee.Id == 0)
            {
                _EmployeesData.AddNew(
                new Employee
                {
                    Id = _EmployeesData.Employees.Count() + 1,
                    FirstName = employee.FirstName,
                    SurName = employee.SurName,
                    Age = employee.Age
                });
            }
            else
            {
                _EmployeesData.Edit(employee);
            }

            return Redirect("/Employes/Index");
        }
    }
}