using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Implementations;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class SideMenuViewComponent : ViewComponent
    {

        private readonly IServiceCategoryData _Categories;

        public SideMenuViewComponent(IServiceCategoryData categoryData)
        {
            _Categories = categoryData;
        }

        public IViewComponentResult Invoke()
        {
            return View(_Categories.Categories);
        }
    }
}
