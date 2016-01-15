using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class CarObs:Car,IObserwowany
    {
        private List<IObserwator> _listaObserwatorow = new List<IObserwator>();


        public void dodajObserwatora(IObserwator o)
        {
            _listaObserwatorow.Add(o);
        }

        public void usunObserwator(IObserwator o)
        {
            _listaObserwatorow.Remove(o);
        }

        public void powiadomObserwatorow()
        {
            foreach (var item in _listaObserwatorow)
            {
                item.aktualizacjaDanych();
            }
        }
        public int PobierzIlosc() { return base.Count; }
        public void ZmierzIlosc(int count)
        {
            base.Count=count;
        }


       
    }
}