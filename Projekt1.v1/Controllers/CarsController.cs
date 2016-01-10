﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Projekt1.v1.Models;
using System.Globalization;

namespace Projekt1.v1.Controllers
{
    public class CarsController : Controller
    {
        private RentACarEntities db = new RentACarEntities();

        public ActionResult Index()
        {
            ViewData["cars"] = db.CarBodyType;
            return View();
        }

        public ActionResult Car_Read([DataSourceRequest]DataSourceRequest request)
        {
           
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car;
            DataSourceResult result = ncar.ToDataSourceResult(request, car => new {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Mileage = car.Mileage,
                ReleaseDate = car.ReleaseDate,
                Count = car.Count,
                BodyTypeId= car.BodyTypeId,
                AddDate = car.AddDate,
                ModifiedDate = car.ModifiedDate,
            });

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Borrow> borrow = db.Borrow.Where(w => w.CarId == id).ToList();
            if (borrow == null)
            {
                return HttpNotFound();
            }
            
            return PartialView("_PopupDetails",borrow);
        }

        public ActionResult Create()
        {
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            return PartialView("_PopupCreate");
        }

        // POST: Cars2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,Brand,Model,Mileage,ReleaseDate,BodyTypeId,Count,AddDate,ModifiedDate")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Car.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name", car.BodyTypeId);
            return View(car);
        }

        // GET: Cars2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Car.Find(id);
            
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name", car.BodyTypeId);
            return PartialView("_Popup", car);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,Brand,Model,Mileage,ReleaseDate,BodyTypeId,Count,AddDate,ModifiedDate")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Secound");
            }
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name", car.BodyTypeId);
            return PartialView("_Popup",car);
        }

        public ActionResult CloseWindow()
        {
            return View();
        }

        public ActionResult Secound()
        {
            return View();
        }
    }
}
