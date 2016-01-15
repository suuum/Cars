using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class Filter
    {
        RentACarEntities db = new RentACarEntities();
        public Filter() { }
        public virtual List<Car> Name() { 
            return db.Car.ToList() ;
        }
    }
}