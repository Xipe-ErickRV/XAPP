using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xapp.Domain.DTOs.Example;

namespace Xapp.Web.Controllers
{
    public class ExampleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var output = new ExampleInput();
            return View(output);
        }

        [HttpPost]
        public IActionResult Index(ExampleInput input)
        {
            //aquí se manda a llamar a los endpoints que estan desarrollando en el proyecto API
            var output = input;
            return View(output);
        }

    }
}
