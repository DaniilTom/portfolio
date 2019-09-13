using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Components
{
    public class TopMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //if (User.Identity.IsAuthenticated)
                return View();
            //else return Content("NO.");
        }
    }
}
