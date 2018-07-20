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
            List<Client> allClients = Client.GetAll();           
            return View(allClients);
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

        [HttpGet("/client/all/{id}/details")]
        public ActionResult Details(int id)
        {
            Client thisClient = Client.Find(id);
            return View("Details", thisClient);
        }

        [HttpPost("client/all/delete")]
        public ActionResult DeleteAll()
        {
            Client.DeleteAll();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/client/all/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Client selectedClient = Client.Find(id);
            selectedClient.DeleteClient();
            return RedirectToAction("All");
        }

        [HttpGet("client/all/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            List<Stylist> allStylists = Stylist.GetAll();
            Client selectedClient = Client.Find(id);
            model.Add("allStylists", allStylists);
            model.Add("selectedClient", selectedClient);
            return View(model);
        }

        [HttpPost("client/all/{id}/update")]
        public ActionResult Update(int id, string name, int stylistId)
        {
            Client selectedClient = Client.Find(id);
            selectedClient.Update(name, stylistId);
            return RedirectToAction("Details", new { id = id });
        }
    }
}
