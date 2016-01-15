using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class SilverUser : User, IUser
    {
        RentACarEntities db = new RentACarEntities();
        List<Car> list;
        public SilverUser()
        {
            list = db.Car.ToList();
        }
        public List<Car> CarPriceLimit()
        {
            list = list.Where(x => x.Price <= 1000).ToList();
            return list;
        }

        public List<Car> ProductionDateLimit()
        {
            list=list.Where(x=>x.ReleaseDate <= DateTime.Parse("2015-01-01")).ToList();
            return list;
        }
    }
}