using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpGet("/stylist/all")]
        public ActionResult All()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpGet("/stylist/add")]
        public ActionResult AddForm()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpPost("/stylist/add")]
        public ActionResult Add(string name, string details)
        {
            int id = 0;
            Stylist newStylist = new Stylist(id, name, details);
            newStylist.Save();
            return RedirectToAction("All");
        }

        [HttpPost("stylist/all/delete")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Index", "Home")
        }

        [HttpGet("/stylist/all/{id}/details")]
        public ActionResult Details(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            return View("Details", thisStylist);
        }
    }
}
