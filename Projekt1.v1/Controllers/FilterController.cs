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
namespace Projekt1.v1.Controllers
{
    public class FilterController : Controller
    {
        private RentACarEntities db = new RentACarEntities();
        // GET: Filter
        public ActionResult Index()
        {
            return View();
        }

        // GET: Filter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Filter/Create
        public ActionResult UsersFilter()
        {
            return PartialView("_UserFilter");
        }

        public ActionResult CarFilter()
        {
            ViewData["cars"] = db.CarBodyType;
            return PartialView("_CarFilter");

        }
        public ActionResult FilterMenuCustomization()
        {

            return Json(db.User.Select(e => e.LastName).Distinct(), JsonRequestBehavior.AllowGet);
        } 
        // POST: Filter/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Filter/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Filter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult User_Read([DataSourceRequest]DataSourceRequest request)
        {

            IQueryable<User> nuser = db.User;
            List<UserBorrow> Userborrow = new List<UserBorrow>();
            foreach (var user in nuser)
            {
                Userborrow.Add(new UserBorrow
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirhtDate = user.BirhtDate,
                    Email = user.Email,
                    Phone = user.Phone,
                    AddDate = user.AddDate,
                    ModifiedDate = user.ModifiedDate,
                    IsActive = user.IsActive,
                    Count = user.Borrow.Count


                });
            }
            DataSourceResult result = Userborrow.ToDataSourceResult(request, user => new
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirhtDate = user.BirhtDate,
                Email = user.Email,
                Phone = user.Phone,
                AddDate = user.AddDate,
                ModifiedDate = user.ModifiedDate,
                IsActive = user.IsActive,
                Count = user.Count
            });

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Car_Read([DataSourceRequest]DataSourceRequest request)
        {

            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car;
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = Carborrow.ToDataSourceResult(request, car => new
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Mileage = car.Mileage,
                ReleaseDate = car.ReleaseDate,
                Count = car.Count,
                BodyTypeId = car.BodyTypeId,
                AddDate = car.AddDate,
                ModifiedDate = car.ModifiedDate,
                BorrowCount = car.BorrowCount
            });

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        //Model 
        public ActionResult Car_ReadMod([DataSourceRequest]DataSourceRequest request, string name)
        {

            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(name));
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModBodyType([DataSourceRequest]DataSourceRequest request, string name)
        {
            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[1]);
            string model = splitname[0];
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.BodyTypeId == bodytypeid);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModBodyTypeMin([DataSourceRequest]DataSourceRequest request, string name)
        {
            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[1]);
            string model = splitname[0];
            DateTime min = DateTime.Parse(splitname[2]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.BodyTypeId == bodytypeid && x.AddDate >= min);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModBodyTypeMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[1]);
            string model = splitname[0];
            DateTime max = DateTime.Parse(splitname[2]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.BodyTypeId == bodytypeid && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModBodyTypeMinMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[1]);
            string model = splitname[0];
            DateTime min = DateTime.Parse(splitname[2]);
            DateTime max = DateTime.Parse(splitname[3]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.BodyTypeId == bodytypeid && x.AddDate >= min && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModMin([DataSourceRequest]DataSourceRequest request, string name)
        {
            string[] splitname = name.Split(',');
            string model = splitname[0];
            DateTime min = DateTime.Parse(splitname[1]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.AddDate >= min);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            string model = splitname[0];
            DateTime max = DateTime.Parse(splitname[1]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadModMinMax([DataSourceRequest]DataSourceRequest request, string name)
        {
            string[] splitname = name.Split(',');
            string model = splitname[0];
            DateTime min = DateTime.Parse(splitname[1]);
            DateTime max = DateTime.Parse(splitname[2]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.Model.Contains(model) && x.AddDate >= min && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //bodytype
        public ActionResult Car_ReadBodyType([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[0]);
            
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.BodyTypeId == bodytypeid);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //
        public ActionResult Car_ReadBodyTypeMin([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[0]);
            DateTime min = DateTime.Parse(splitname[1]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.BodyTypeId == bodytypeid && x.AddDate >= min);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Car_ReadBodyTypeMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[0]);
            DateTime max = DateTime.Parse(splitname[1]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.BodyTypeId == bodytypeid && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Car_ReadBodyTypeMinMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            int bodytypeid = int.Parse(splitname[0]);
            DateTime min = DateTime.Parse(splitname[1]);
            DateTime max = DateTime.Parse(splitname[2]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.BodyTypeId == bodytypeid && x.AddDate >= min && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Time
        public ActionResult Car_ReadMin([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            DateTime min = DateTime.Parse(splitname[0]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.AddDate >= min);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Car_ReadMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            DateTime max = DateTime.Parse(splitname[0]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x => x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Car_ReadMinMax([DataSourceRequest]DataSourceRequest request, string name)
        {

            string[] splitname = name.Split(',');
            DateTime min = DateTime.Parse(splitname[0]);
            DateTime max = DateTime.Parse(splitname[1]);
            ViewBag.BodyTypeId = new SelectList(db.CarBodyType, "BodyTypeId", "Name");
            IQueryable<Car> ncar = db.Car.Where(x =>  x.AddDate >= min && x.AddDate <= max);
            List<CarBorrow> Carborrow = new List<CarBorrow>();
            foreach (var car in ncar)
            {
                int count = db.Borrow.Where(x => x.CarId == car.CarId).Count();

                Carborrow.Add(new CarBorrow
                {
                    CarId = car.CarId,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage,
                    ReleaseDate = car.ReleaseDate,
                    Count = car.Count,
                    BodyTypeId = car.BodyTypeId,
                    AddDate = car.AddDate,
                    ModifiedDate = car.ModifiedDate,
                    BorrowCount = count
                });
            }

            DataSourceResult result = GetDataSourceResult(request, Carborrow);

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        private static DataSourceResult GetDataSourceResult(DataSourceRequest request, List<CarBorrow> Carborrow)
        {
            DataSourceResult result = Carborrow.ToDataSourceResult(request, car => new
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Mileage = car.Mileage,
                ReleaseDate = car.ReleaseDate,
                Count = car.Count,
                BodyTypeId = car.BodyTypeId,
                AddDate = car.AddDate,
                ModifiedDate = car.ModifiedDate,
                BorrowCount = car.BorrowCount
            });
            return result;
        }

        public ActionResult GetCarBodyTypes()
        {
            IQueryable<CarBodyType> ncar = db.CarBodyType;
            var result = ncar.Select(car => new
            {
                BodyTypeId = car.BodyTypeId,
                Name = car.Name
            });

            return Json(result,JsonRequestBehavior.AllowGet);
        }


    }
}
