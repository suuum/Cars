using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public static class CarsList 
    {
        private static List<Car> BorrowCars = new List<Car>();

        public static void Add(Car item)
        {
            BorrowCars.Add(item);
        }
        public static List<Car> GetList()
        {
            return BorrowCars;
        }
        public static Car GetElementAt(int index)
        {
            return BorrowCars[index];
        }
        public static void Remove(Car item)
        {
            BorrowCars.Remove(item);

        }

        public static void Dispose()
        {
            BorrowCars.Clear();
            BorrowCars = new List<Car>();
        }
    }
}