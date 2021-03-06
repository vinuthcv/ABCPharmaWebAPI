﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Entities;
using System.Net.Http.Formatting;

namespace PharmaWeb.Controllers
{
    public class PharmaController : Controller
    {
        // GET: Pharma
        public ActionResult Index(string search = "")
        {
            var medicineModels = new List<Models.Medicine>();
            using (var client = new HttpClient { BaseAddress = new Uri("https://localhost:44377/api/") })
            {
                var result = client.GetAsync("medicine/?search=" + search).Result;
                if (result.IsSuccessStatusCode)
                {
                    var medicines = result.Content.ReadAsAsync<List<Medicine>>().Result;


                    foreach (var med in medicines)
                    {
                        var medModel = new Models.Medicine
                        {
                            Id = med.Id,
                            Company = med.Company,
                            Expiry = med.Expiry,
                            Name = med.Name,
                            Quantity = med.Quantity
                        };

                        medicineModels.Add(medModel);
                    }
                }
            }
            return View(medicineModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new Models.Medicine();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Models.Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                if (medicine != null && medicine.Expiry <= DateTime.Now.AddDays(15))
                {
                    ModelState.AddModelError("Expiry", "Medicine with expiry date less than 15 days cannot be added");
                }

                if (ModelState.IsValid)
                {
                    Entities.Medicine entity = new Medicine
                    {
                        Company = medicine.Company,
                        Expiry = medicine.Expiry,
                        Name = medicine.Name,
                        Quantity = medicine.Quantity
                    };

                    using (var client = new HttpClient { BaseAddress = new Uri(@"https://localhost:44377/api/") })
                    {
                        var result = client.PostAsJsonAsync("medicine", entity).Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");

                        }
                        ModelState.AddModelError("API", "Error in request, please contact us for assistance");
                        return View(medicine);
                    }
                }
            }

            return View(medicine);

        }

        public ActionResult Details(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(@"https://localhost:44377/api/") })
            {
                var result = client.GetAsync("medicine/" + id).Result;

                if (result.IsSuccessStatusCode)
                {
                    var entity = result.Content.ReadAsAsync<Entities.Medicine>().Result;

                    var model = new Models.Medicine
                    {
                        Company = entity.Company,
                        Expiry = entity.Expiry,
                        Id = entity.Id,
                        Name = entity.Name,
                        Quantity = entity.Quantity
                    };
                    return View(model);
                }
            }

            return View();
        }

        // GET: Sample/Edit/5
        public ActionResult Edit(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(@"https://localhost:44377/api/") })
            {
                var result = client.GetAsync("medicine/" + id).Result;

                if (result.IsSuccessStatusCode)
                {
                    var entity = result.Content.ReadAsAsync<Entities.Medicine>().Result;

                    var model = new Models.Medicine
                    {
                        Company = entity.Company,
                        Expiry = entity.Expiry,
                        Id = entity.Id,
                        Name = entity.Name,
                        Quantity = entity.Quantity
                    };
                    return View(model);
                }
            }

            return View();
        }

        // POST: Sample/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Medicine medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (medicine != null && medicine.Expiry <= DateTime.Now.AddDays(15))
                    {
                        ModelState.AddModelError("Expiry", "Medicine with expiry date less than 15 days cannot be added");
                    }

                    if (ModelState.IsValid)
                    {
                        Entities.Medicine entity = new Medicine
                        {
                            Company = medicine.Company,
                            Expiry = medicine.Expiry,
                            Name = medicine.Name,
                            Quantity = medicine.Quantity,
                            Id = id
                        };

                        using (var client = new HttpClient { BaseAddress = new Uri(@"https://localhost:44377/api/") })
                        {
                            var result = client.PutAsJsonAsync("medicine", entity).Result;
                            if (result.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");

                            }
                            ModelState.AddModelError("API", "Error in request, please contact us for assistance");
                            return View(medicine);
                        }
                    }
                }
            }
            catch
            {
                return View();
            }
            return View(medicine);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(@"https://localhost:44377/api/") })
            {
                var result = client.GetAsync("medicine/" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    var entity = result.Content.ReadAsAsync<Entities.Medicine>().Result;

                    var model = new Models.Medicine
                    {
                        Company = entity.Company,
                        Expiry = entity.Expiry,
                        Id = entity.Id,
                        Name = entity.Name,
                        Quantity = entity.Quantity
                    };

                    return View(model);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, Models.Medicine medicine)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(@"https://localhost:44377/api/") })
            {
                var result = client.DeleteAsync("medicine/" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(medicine);
            }
        }

    }
}