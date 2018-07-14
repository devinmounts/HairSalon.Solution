using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Dynamic;


namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/client/all")]
        public ActionResult All()
        {
            dynamic mymodel = new ExpandoObject();
            List<Client> allClients = Client.GetAll();
            List<Stylist> allStylists = Stylist.GetAll();
           
            return View(mymodel);
        }

        [HttpGet("/client/add")]
        public ActionResult AddForm()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpPost("/client/add")]
        public ActionResult Add(string name, int stylistId)
        {
            int id = 0;
            Client newClient = new Client(id, name, stylistId);
            newClient.Save();
            return RedirectToAction("All");
        }

    }
}
