using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Dynamic;


namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/specialty/all")]
        public ActionResult All()
        {
            List<Specialty> allSpecialties = Specialty.GetAll();           
            return View(allSpecialties);
        }

        [HttpGet("/specialty/add")]
        public ActionResult AddForm()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpPost("/specialty/add")]
        public ActionResult Add(string description)
        {
            int id = 0;
            Specialty newSpecialty = new Specialty(description, id);
            newSpecialty.Save();
            return RedirectToAction("All");
        }

        [HttpGet("/specialty/all/{id}/details")]
        public ActionResult Details(int id)
        {
            Specialty thisSpecialty = Specialty.Find(id);
            return View("Details", thisSpecialty);
        }

        [HttpPost("specialty/all/delete")]
        public ActionResult DeleteAll()
        {
            Specialty.DeleteAll();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/specialty/all/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Specialty selectedSpecialty = Specialty.Find(id);
            selectedSpecialty.DeleteSpecialty();
            return RedirectToAction("All");
        }

        [HttpGet("specialty/all/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            List<Stylist> allStylists = Stylist.GetAll();
            Specialty selectedSpecialty = Specialty.Find(id);
            model.Add("allStylists", allStylists);
            model.Add("selectedSpecialty", selectedSpecialty);
            return View(model);
        }

        [HttpPost("specialty/all/{id}/update")]
        public ActionResult Update(int id, string description, int stylistId)
        {
            Specialty selectedSpecialty = Specialty.Find(id);
            selectedSpecialty.Update(description);
            Stylist foundStylist = Stylist.Find(stylistId);
            selectedSpecialty.AddStylist(foundStylist);
            return RedirectToAction("Details", new { id = id });
        }
    }
}
