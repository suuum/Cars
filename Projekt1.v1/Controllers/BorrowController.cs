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
    public class BorrowController : Controller
    {
        private RentACarEntities db = new RentACarEntities();

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult UserHaveCar()
        {

            return View();
        }
        [HttpGet]
        public ActionResult UserCars(int id)
        {
           ViewBag.id=id;
            ViewData["cars"] = db.CarBodyType;
            return View();
        }

        public ActionResult Rent()
        {
            return View();
        }
        public ActionResult Return(int id)
        {
          Borrow item =  db.Borrow.Where(x => x.BorrowId == id).FirstOrDefault();
          
         db.Borrow.Attach(item);
         db.Entry(item).Property(x => x.IsReturned).CurrentValue = true;
          Car caritem =  db.Car.Where(x => x.CarId == item.CarId).FirstOrDefault();

          Car car = db.Car.Find(item.CarId);
          if (car == null)
          {
              return HttpNotFound();
          }
          car.Count +=1;
          db.Car.Attach(car);
          db.Entry(car).Property(x => x.Count).IsModified = true;
          db.SaveChanges();
        
            ViewData["cars"] = db.CarBodyType;
            return RedirectToAction("Index");
        }
        public ActionResult ReturnCars(string choosencar)
        {
                    string[] stringlist = choosencar.Split(',');
                    List<int> idlist = new List<int>();
            foreach (string item in stringlist)
            {
             idlist.Add(int.Parse(item));
            }
            foreach (int id in idlist)
            {
                Borrow item = db.Borrow.Where(x => x.BorrowId == id).FirstOrDefault();
                db.Borrow.Attach(item);
                db.Entry(item).Property(x => x.IsReturned).CurrentValue = true;
                Car caritem = db.Car.Where(x => x.Count == id).FirstOrDefault();
                db.Car.Attach(caritem);
                db.Entry(caritem).Property(x => x.Count).CurrentValue += 1;
            }
            db.SaveChanges();
            ViewData["cars"] = db.CarBodyType;
            return RedirectToAction("Index");
        }
        public JsonResult Borrow_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Borrow> newborrow = db.Borrow.Where(x=>x.IsReturned==false);
            DataSourceResult result = newborrow.ToDataSourceResult(request, borrow => new {
                BorrowId = borrow.BorrowId,
                CarId = borrow.CarId,
                FromDate = borrow.FromDate,
                ToDate = borrow.ToDate,
                IsReturned = borrow.IsReturned,
            });

            return Json(result);
        }
        public JsonResult BorrowUser_Read([DataSourceRequest]DataSourceRequest request, int? id)
        {
            IQueryable<Borrow> newborrow = db.Borrow.Where(x => x.IsReturned == false && x.UserId==id);
            DataSourceResult result = newborrow.ToDataSourceResult(request, borrow => new
            {
                BorrowId = borrow.BorrowId,
                CarId = borrow.CarId,
                FromDate = borrow.FromDate,
                ToDate = borrow.ToDate,
                IsReturned = borrow.IsReturned,
            });

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Car_Read([DataSourceRequest]DataSourceRequest request, int? id)
        {

            IQueryable<Borrow> newborrow = db.Borrow.Where(x=> x.UserId == id);
            DataSourceResult result = newborrow.ToDataSourceResult(request, borrow => new
            {
                BorrowId = borrow.BorrowId,
                CarId = borrow.CarId,
                FromDate = borrow.FromDate,
                ToDate = borrow.ToDate,
                IsReturned = borrow.IsReturned,
            });


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult User_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<User> nuser = db.User.Include(x => x.Borrow);
            nuser = db.User.Where( x=>x.Borrow.Count!=0);
            DataSourceResult result = nuser.ToDataSourceResult(request, user => new
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
            });

            return Json(result);
        }

        public ActionResult BorrowCars(int userid, string select,string fromDate,string toDate)
        {
            string[] stringlist = select.Split(',');
            List<int> idlist = new List<int>();
            foreach (string item in stringlist)
            {
                idlist.Add(int.Parse(item));
            }
            foreach (int id in idlist)
            {
                DateTime from = DateTime.Parse(fromDate);
                DateTime to = DateTime.Parse(toDate); 
                db.Borrow.Add(new Borrow() { 
                CarId = id,
                UserId = userid,
                FromDate =from,
                ToDate = to,
                IsReturned=false
                });

                Car caritem = db.Car.Where(x => x.CarId == id).FirstOrDefault();
                db.Car.Attach(caritem);
                caritem.Count -= 1;
                var entry = db.Entry(caritem);
                entry.Property(e => e.Count).IsModified = true;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
           
          
                }

        public ActionResult BorrowCarWindow(int? id)
        {
            MenuViewModel menu = new MenuViewModel();
            menu.MenuLevel1 = db.User.ToList().Select(x => new SelectListItem
            {
                Value=x.UserId.ToString(),
                Text = x.FirstName + " " + x.LastName


            }).ToList();
            menu.MenuLevel2 = new List<SelectListItem>();
            return PartialView("_PopupGetCar", menu);
        }

        [HttpGet]
        public ActionResult fillcatlevel2(int id)
        {
            MenuViewModel menu = new MenuViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            List<Car> car = new List<Car>();
            List<int> avaliablecar = new List<int>();
            var listcar=db.Car.Where(x=>x.Count!=0).ToList();
            avaliablecar= db.Borrow.Where(x=>x.UserId==id).Select(x => x.CarId).ToList();

            car = listcar;
            foreach (var item in avaliablecar)
            {
                car.Remove(db.Car.Where(x => x.CarId == item).FirstOrDefault());
            }


            
                list = car.Select(c => new SelectListItem
                {
                    Value = c.CarId.ToString(),
                    Text = c.Brand + " " + c.Model
                }).ToList();
               
               
            
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult fillnextcar(int id,string select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Car> car = new List<Car>();
            
            var listcar = db.Car.ToList();
            
            string[] carlistsplit = select.Split(' ');
            List<int> carlist=new List<int>();
            for (int i = 0; i <carlistsplit.Length ; i++)
            {
                carlist.Add(int.Parse(carlistsplit[i]));
            }
            foreach (var item in carlist)
            {
                car.Add(db.Car.Where(x => x.CarId == item).FirstOrDefault());
            }

            list = car.Select(c => new SelectListItem
            {
                Value = c.CarId.ToString(),
                Text = c.Brand + " " + c.Model
            }).ToList();



            return Json(list, JsonRequestBehavior.AllowGet);

        }

 
   //        int id= int.Parse(Request.QueryString["id"]);
     

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
