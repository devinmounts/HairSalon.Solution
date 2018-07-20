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


        [HttpGet("/stylist/all/{id}/details")]
        public ActionResult Details(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            return View("Details", thisStylist);
        }

        [HttpPost("stylist/all/delete")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/stylist/all/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Stylist selectedStylist = Stylist.Find(id);
            selectedStylist.DeleteStylist();
            return RedirectToAction("All");
        }

        [HttpGet("stylist/all/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            List<Specialty> allSpecialties = Specialty.GetAll();
            Stylist selectedStylist = Stylist.Find(id);
            model.Add("allSpecialties", allSpecialties);
            model.Add("selectedStylist", selectedStylist);
            return View(model);
        }

        [HttpPost("stylist/all/{id}/update")]
        public ActionResult Update(int id, string name, string details, int specialtyId)
        {
            Stylist selectedStylist = Stylist.Find(id);
            selectedStylist.Update(name, details);

            Specialty foundSpecialty = Specialty.Find(specialtyId);
            selectedStylist.AddSpecialty(foundSpecialty);
            return RedirectToAction("Details", new { id = id });
        }
    }
}
