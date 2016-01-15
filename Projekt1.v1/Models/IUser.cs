using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt1.v1.Models
{
    public interface IUser
    {
        List<Car> CarPriceLimit();
        List<Car> ProductionDateLimit();

    }
}
