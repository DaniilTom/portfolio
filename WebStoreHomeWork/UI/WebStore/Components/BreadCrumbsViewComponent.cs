using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var BreadCrumbs = new BreadCrumbsDTO();

            BreadCrumbs.Crumbs.Add(ViewContext.RouteData.Values["Controller"].ToString());
            BreadCrumbs.Crumbs.Add(ViewContext.RouteData.Values["Action"].ToString());

            return View(BreadCrumbs);
        }
    }
}
