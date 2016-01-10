using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Projekt1.v1.Models
{
    public class MenuViewModel
    {
        public List<SelectListItem> MenuLevel1
        {
            get;
            set;
        }
        public List<SelectListItem> MenuLevel2
        {
            get;
            set;
        }
        public int? CategoryId1 { get; set; }

        public int? CategoryId2 { get; set; }
    }
}