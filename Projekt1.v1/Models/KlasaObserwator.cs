using Projekt1.v1.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class KlasaObserwator:IObserwator
    {
        
        //referencja do obserwowanego obiektu
        private CarObs kontrolerilosci;
 
        //wraz z definicja obiektu przypisywany mu jest czujnik
        public KlasaObserwator(CarObs o)
        {
            kontrolerilosci = o;
        }
        public void aktualizacjaDanych()
        {
            MailSender mail = MailSender.Instance;
            if (kontrolerilosci.Count == 0)
            {
                mail.SendMail("fum1993@gmail.com", "Brak samochodu marki" + kontrolerilosci.Brand + " model:" + kontrolerilosci.Model + "w magazynie");

            }
        }
    }
}