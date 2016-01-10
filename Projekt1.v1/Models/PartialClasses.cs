using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt1.v1.Models
{
    public class PartialClasses
    {
        [MetadataType(typeof(UserMetadata))]
        public partial class User
        {
        }

        [MetadataType(typeof(CarMetadata))]
        public partial class Car
        {
        }
    }
}