using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.controllers
{
    public class TestController : Controller
    {
        private readonly IValueService _ValuesService;

        public TestController(IValueService ValuesService)
        {
            _ValuesService = ValuesService;
        }

        public void Index()
        {
            string resp = _ValuesService.GetOk().Result;
            HttpContext.Response.WriteAsync(resp.Replace("\"","")); // почему-то приписывает кавычки в начало и конец
            //return View(_ValuesService.GetOk());
        }
    }
}