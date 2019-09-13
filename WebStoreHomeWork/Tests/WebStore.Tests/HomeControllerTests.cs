using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _Controller;

        [TestInitialize]
        public void Init()
        {
            _Controller = new HomeController(null, null);
        }

        [TestMethod]
        public void Index_Returns_ViewResult()
        {
            Assert.IsType<ViewResult>(_Controller.Index());
        }

        [TestMethod]
        public void Catalog_Returns_ViewResult()
        {
            Assert.IsType<ViewResult>(_Controller.Catalog());
        }

        [TestMethod]
        public void Contact_Returns_ViewResult()
        {
            var logger_mock = new Mock<ILogger<HomeController>>();

            Assert.IsType<ViewResult>(_Controller.Contact(logger_mock.Object));
        }
    }
}
