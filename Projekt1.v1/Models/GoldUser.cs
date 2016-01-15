using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class GoldUser:User,IUser

    {
        List<Car> list;
        RentACarEntities db = new RentACarEntities();
        public GoldUser() { list = db.Car.ToList(); }
        public List<Car> CarPriceLimit()
        {
            return list;
        }

        public List<Car> ProductionDateLimit()
        {
            return list;
        }
    }
}