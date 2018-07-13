﻿using System;
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
            return View(result);
        }

        [HttpGet("/client/add")]
        public ActionResult AddForm()
        {
            
            return View();
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
