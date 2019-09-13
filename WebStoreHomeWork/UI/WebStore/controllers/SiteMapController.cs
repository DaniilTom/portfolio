using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;

namespace WebStore.controllers
{
    public class SiteMapController : Controller
    {
        public SiteMapController() { }

        public IActionResult Index()
        {
            var nodes = new List<SitemapNode>();

            Assembly asm = Assembly.GetExecutingAssembly();

            var action_list = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute))
                                                    && !method.IsConstructor && method.ReturnType == typeof(IActionResult));

            foreach(var action in action_list)
            {
                nodes.Add(new SitemapNode(Url.Action(action.Name, action.ReflectedType.Name.Replace("Controller", ""))));
            }


            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}