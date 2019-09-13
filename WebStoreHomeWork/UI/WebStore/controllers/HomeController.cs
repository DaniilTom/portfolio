using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartBreadcrumbs.Attributes;
using WebStore.Domain.ViewModel;
using WebStore.Interfaces.Services;

namespace WebStore.controllers
{
    
    //[Controller]
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;
        private readonly IServiceProductData _ProductData;

        public HomeController(IConfiguration Configuration, IServiceProductData ProductData)
        {
            _Configuration = Configuration;
            _ProductData = ProductData;
        }

        [DefaultBreadcrumb("Home")]
        public IActionResult Index()
        {
            //return Content("Hello World!!");
            return View();
        }

        [Breadcrumb("Contact")]
        public IActionResult Contact([FromServices] ILogger<HomeController> log)
        {
            log.LogWarning("Контакты");
            return View();
        }

        [Breadcrumb("Catalog", FromAction = "Contact")] //FromAction просто так, посмотреть
        public IActionResult Catalog(int page = 0)
        {
            ViewBag.PageSize = int.Parse(_Configuration["PageSize"]);
            ViewBag.Page = page;
            return View();
        }

        [Breadcrumb("Catalog", FromAction = "Contact")] //FromAction просто так, посмотреть
        public IActionResult GetPartialCatalog(int page = 0)
        {
            int pageSize = int.Parse(_Configuration["PageSize"]);
            var items = GetProducts(page, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            
            return PartialView("Partial/_CatalogPartial", items);
        }

        private IEnumerable<MicrocontrollerViewModel> GetProducts(int page, int pageSize)
        {
            int count = _ProductData.Products.Count();
            ViewBag.PageCount = (int)Math.Ceiling((double)count / pageSize);

            int skip = 0;

            if (page != 0)
                skip = (page - 1) * pageSize;
            else
                pageSize = count;

            var mc = _ProductData.Products.Skip(skip).Take(pageSize);
            var desc = _ProductData.DetailedDescription;

            return mc.Select(p => new MicrocontrollerViewModel
            {
                ProductBase = p,
                MCDescription = desc.FirstOrDefault(d => p.Id == d.ProductId) ?? new Domain.DTO.MCDescriptionDTO { DetailedDesription = "Нет описания;" }
            });
        }

        public IActionResult Test()
        {
            Task.Delay(2000);
            return Json(new { Message = "OK" });
        }

        public IActionResult SignalRTest() => View();
    }
}