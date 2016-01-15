using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class Decorator:Filter
    {
        private Filter f;
        private List<Car> name;

        public Decorator(Filter f) {
            this.f = f;
        }

        public override List<Car> Name()
        {
            
            return this.f.Name();
        }

         

    }
}