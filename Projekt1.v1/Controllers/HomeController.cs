using Projekt1.v1.Models;
using Projekt1.v1.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt1.v1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public ActionResult Index()
        {
            MailSender x = MailSender.Instance;
            
           List<User> list= x.CheckDateReturnDate().ToList();
           x.SendMail("fum1993@gmail.com", "Super partia kurwo");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
           
            return View();
        }
        public ActionResult Dec()
        {
            ViewBag.Message = "Your contact page.";
            Projekt1.v1.Models.Filter data = new CarDateTime(new CarModel(new DataFilter(), "A4"), "2010-09-11");

            IUser user = new GoldUser();
            UserFactory factory = new UserFactory(user);

            factory.SetupCarList();
            return View(data.Name());
        }
    }
}