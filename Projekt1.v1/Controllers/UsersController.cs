using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Projekt1.v1.Models;

namespace Projekt1.v1.Controllers
{
    public class UsersController : Controller
    {
        private RentACarEntities db = new RentACarEntities();

        // GET: Users
        public ActionResult Index()
        {
            var user = db.User.Include(d=>d.Borrow);
            
            return View(user.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include =                          "FirstName,LastName,BirhtDate,Email,Phone,AddDate,ModifiedDate,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);

        }
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User newUser = db.User.Find(id);
            if (newUser == null)
            {
                return HttpNotFound();
            }
            return View(newUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,BirhtDate,Email,Phone,AddDate,ModifiedDate,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User newUser = db.User.Find(id);
            if (newUser == null)
            {
                return HttpNotFound();
            }
            db.User.Attach(newUser);
            db.Entry(newUser).Property(x => x.IsActive).CurrentValue = true;
            db.SaveChanges();
            return RedirectToAction("Index");
         
        }
        
    }
}
