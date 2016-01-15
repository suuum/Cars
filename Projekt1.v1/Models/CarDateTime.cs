using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class CarDateTime:Decorator
    {
        private List<Car> list;
        private Filter filter;
        private string kayWord;
        public CarDateTime(Filter f,string kayWord):base(f)
        {
            this.filter = f;
            this.kayWord = kayWord;
        }
        public override List<Car> Name()
        {
            
            this.list = filter.Name();
            list = list.Where(x => x.ReleaseDate ==DateTime.Parse(kayWord)).ToList() ;
            return this.list;
        }
    }
}