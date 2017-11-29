using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private SessionController sessionController;
        public IActionResult Index()
        {
            sessionController = new SessionController(HttpContext);

            if (sessionController.IsLoggedIn())
                return RedirectToAction("Spots", "Spots");

            return View();
        }
    }
}
