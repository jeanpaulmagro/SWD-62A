using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class SupportController : Controller
    {
        //this method will be called using a get request and its purpose is to load the page
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public object Contact(string email,string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                ViewData["warning"] = "Question was left empty";
            }
            else
            {
                ViewData["feedback"] = "Thank you for your query, we will get back to you asap";
            }

            return View();
        }
    }
}
