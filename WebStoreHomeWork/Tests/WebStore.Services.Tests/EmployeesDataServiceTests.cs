using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WebStore.Models;
using WebStore.Services.InMemory;

namespace WebStore.Services.Tests
{
    [TestClass]
    public class EmployeesDataServiceTests
    {
        public EmployeesDataService _Data;

        [TestInitialize]
        public void Init()
        {
            _Data = new EmployeesDataService();
        }

        [TestMethod]
        public void AddNew_Correct()
        {
            const int expected_id = 3;
            const int expected_age = 30;
            const string expected_name = "TestName";
            Employee emp = new Employee
            {
                Id = expected_id,
                FirstName = expected_name,
                Age = expected_age
            };

            _Data.AddNew(emp);

            Assert.AreEqual(emp, _Data.Employees.First(e => e.Id == expected_id));
        }

        [TestMethod]
        public void Delete_Correct()
        {
            const int test_id = 2;

            _Data.Delete(test_id);

            // выброс исключения в методе Delete не предусмотрен, но так просто легче проверить наличие удаленного элемента
            Assert.ThrowsException<InvalidOperationException>(() => _Data.Employees.First(e => e.Id == test_id));
        }
    }
}
