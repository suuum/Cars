using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class CarBorrow:Car
    {
        public int BorrowCount { get; set; }
    }
}