using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }
    }
}
