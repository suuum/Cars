using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class CarModel:Decorator
    {
        private List<Car> list;
        private Filter filter;
        private string kayWord;
        public CarModel(Filter f,string kayWord):base(f)
        {
            this.filter = f;
            this.kayWord = kayWord;
        }
        public override List<Car> Name()
        {
            this.list = filter.Name();
            list = list.Where(x => x.Model == kayWord).ToList() ;
            return this.list;
        }
    }
}