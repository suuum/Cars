using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class UserFactory
    {
        public IUser category;
        private List<Car> list;
        public UserFactory(IUser _category)
        {
            this.category = _category;
        }

        public IUser getUser()
        {
            return this.category;
        }
        public void SetupCarList()
        {
            list = category.CarPriceLimit();
            list = category.ProductionDateLimit();

        }
      

    }
}