using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;


namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/client/all")]
        public ActionResult All()
        {
            List<Client> result = Client.GetAll();
            return View();
        }

        [HttpGet("/client/add")]
        public ActionResult add()
        {
            List<Stylist> results = Stylist.GetAll();
            return View();
        }
    }
}
