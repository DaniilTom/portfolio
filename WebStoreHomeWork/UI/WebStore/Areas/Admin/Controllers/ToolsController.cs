using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Areas.Admin.ViewModels;
using WebStore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebStore.Domain.Implementations;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Domain.User.AdminRoleName)]
    public class ToolsController : Controller
    {
        private readonly IServiceAllData _db;
        private readonly WebStoreContext _wsContext;

        public ToolsController(IServiceAllData db, WebStoreContext wsContext)
        {
            _db = db;
            _wsContext = wsContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StoreHouse()
        {

            var storeHouseVM = from mic in _db.Products
                               from cat in _db.Categories
                               where mic.CategoryId == cat.Id
                               select new StoreHouseViewModel { Product = mic, Category = cat};

            var categories = _db.Categories;
            ViewBag.Categories = categories;
            ViewData["Cat"] = categories;
            return View(storeHouseVM);
        }

        [HttpPost]
        public IActionResult CreateNew(string name, int price, string catId)
        {
            _db.AddNewProduct(new ProductDTO { Name = name, Price = price, CategoryId = int.Parse(catId)});

            return RedirectToAction("StoreHouse");
        }

        [HttpPost]
        public IActionResult AddDescription(int id, string desc)
        {
            //string[] format_desc = desc.Split("\r\n", StringSplitOptions.RemoveEmptyEntries) ;

            _db.AddNewDescription(new MCDescriptionDTO
            {
                ProductId = id,
                DetailedDesription = desc
            });

            return RedirectToAction("StoreHouse");
        }

        [HttpPost]
        public async Task<IActionResult> UploadImgAsync(IFormFile file, [FromServices] IHostingEnvironment _appEnvironment, int id)
        {
            string path = _appEnvironment.WebRootPath + "/img/" + file.FileName;
            using (var fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }
            _db.Products.First(m => m.Id == id).ImageUrl = "/img/" + file.FileName;

            return RedirectToAction("StoreHouse");
        }

        public IActionResult Orders()
        {
            return View(_db.Orders);
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            var prod = new ProductDTO();
            ViewBag.Categories = _db.Categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            if (id != null)
            {
                ViewBag.Description = _db.DetailedDescription.FirstOrDefault(d => d.ProductId == id).DetailedDesription;//.Replace(";", ";\n");
                prod = _db.Products.FirstOrDefault(p => p.Id == id);
            }
            return View(prod);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductDTO prod, string desc, IFormFile file, [FromServices] IHostingEnvironment _appEnvironment)
        {
            string fileUrl; //= "/img/" + prod.Name + file.FileName.Substring(file.FileName.LastIndexOf('.'));

            Task Download = null;
            if (file != null)
            {
                fileUrl = "/img/" + prod.Name + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                Download = UploadImgAsync(fileUrl, file, _appEnvironment);
            }
            else
            {
                string path = _appEnvironment.WebRootPath;
                string oldName = path + _wsContext.Products.First(p => p.Id == prod.Id).ImageUrl;
                string newName = path + "/img/" + prod.Name + ".jpg";
                fileUrl = "/img/" + prod.Name + ".jpg";
                try
                {
                    System.IO.File.Move(oldName, newName);
                }
                catch (Exception) { }
            }

            ProductBase editProduct;
            if (prod.Id == 0)
            {
                editProduct = new ProductBase()
                {
                    Name = prod.Name,
                    ImageUrl = fileUrl,
                    CategoryId = prod.CategoryId,
                    Category = _wsContext.Categories.First(c => c.Id == prod.CategoryId),
                    Price = prod.Price
                };

                _wsContext.Products.Add(editProduct);
            }
            else
            {
                editProduct = _wsContext.Products.First(p => p.Id == prod.Id);
                editProduct.Name = prod.Name;
                editProduct.ImageUrl = fileUrl;
                editProduct.CategoryId = prod.CategoryId;
                editProduct.Category = _wsContext.Categories.First(c => c.Id == prod.CategoryId);
                editProduct.Price = prod.Price;
            }
            _wsContext.SaveChanges();

            if(!string.IsNullOrEmpty(desc))
            {
                _wsContext.MCDescriptions.Add(new MCDescription
                {
                    Product = editProduct,
                    DetailedDesriptionList = desc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                });
            }
            _wsContext.SaveChanges();

            if(Download != null)Task.WaitAll(Download);

            return RedirectToAction("StoreHouse");
        }

        private async Task UploadImgAsync(string fileName, IFormFile file, IHostingEnvironment _appEnvironment)
        {
            string path = _appEnvironment.WebRootPath +  fileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}