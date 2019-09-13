using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.controllers;
using WebStore.Interfaces.Services;
using WebStore.Models;

using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class EmployeesControllerTests
    {
        private EmployesController _Controller;
        private Mock<IServiceEmployeeData> service_data_mock;

        [TestInitialize]
        public void Init()
        {
            service_data_mock = new Mock<IServiceEmployeeData>();
            service_data_mock
                .Setup(s => s.Employees)
                .Returns(new List<Employee>
                {
                    new Employee { Id = 1, FirstName = "Ivan", Age = 20},
                    new Employee { Id = 2, FirstName = "Johan", Age = 21}
                }.AsEnumerable());

            _Controller = new EmployesController(service_data_mock.Object);
        }

        [TestCleanup]
        public void Clean()
        {
            _Controller = null;
            service_data_mock = null;
        }

        [TestMethod]
        public void Index_Returns_View_with_Model()
        {
            var result = Assert.IsAssignableFrom<ViewResult>(_Controller.Index());

            Assert.IsAssignableFrom<IEnumerable<Employee>>(result.ViewData.Model);
        }

        [TestMethod]
        public void Details_Returns_Correct_ViewResult_with_Employee()
        {
            const int employee_id = 1;
            var result = 
                Assert.IsAssignableFrom<ViewResult>(_Controller.Details(employee_id));
            var employee_result = 
                Assert.IsAssignableFrom<Employee>(result.Model);
                Assert.Equal(employee_id, employee_result.Id);
        }

        [TestMethod]
        public void Details_Returns_Wrong_NotFoundResult()
        {
            const int employee_id = 3;
            Assert.IsAssignableFrom<NotFoundResult>(_Controller.Details(employee_id));
        }

        [TestMethod]
        public void Delete_Returns_RedirectResult()
        {
            const string expected_url = "/Employes/Index";
            const int test_id = 1;
            var result = Assert.IsAssignableFrom<RedirectResult>(_Controller.Delete(test_id));
            Assert.Equal(expected_url, result.Url);
        }

        [TestMethod]
        public void Edit_Correct_Returns_ViewResult_with_Employee()
        {
            const int test_id = 1;
            var viewResult = Assert.IsAssignableFrom<ViewResult>(_Controller.Edit(test_id));
            var employee_result = Assert.IsAssignableFrom<Employee>(viewResult.Model);
            Assert.Equal(test_id, employee_result.Id);
        }

        [TestMethod]
        public void Edit_Wrong_Returns_ViewResult_with_NullModel()
        {
            const int test_id = 0;
            var viewResult = Assert.IsAssignableFrom<ViewResult>(_Controller.Edit(test_id));
            Assert.Null(viewResult.Model);
        }
    }
}
