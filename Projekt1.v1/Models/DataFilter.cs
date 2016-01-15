using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class DataFilter:Filter
    {

        private RentACarEntities db = new RentACarEntities();
        private List<Car> list;
        public DataFilter() {
            this.list = db.Car.ToList();
        }

        public override List<Car> Name() {
            
            return list ;
        
        
        }
    }
}