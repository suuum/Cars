using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Projekt1.v1.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
    }
        public class UserMetadata:IUser
        {


            [Required(ErrorMessage = "Please type first name")]
            [RegularExpression("[a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻ]{3,30}")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Please type last name")]
            [RegularExpression("[a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻ]{3,30}")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Please type your birth date")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime BirhtDate { get; set; }
            
            [Required(ErrorMessage = "Please type e-mail")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Please type phone number")]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
            public string Phone { get; set; }
            
            [Required(ErrorMessage = "Please type add date")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime AddDate { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.DateTime> ModifiedDate { get; set; }
            public bool IsActive { get; set; }



            public List<Car> CarPriceLimit()
            {
                throw new NotImplementedException();
            }

            public List<Car> ProductionDateLimit()
            {
                throw new NotImplementedException();
            }
        }


        [MetadataType(typeof(CarMetadata))]
        public partial class Car
        {
        }
        public partial class CarMetadata
        {


            [Required(ErrorMessage = "Please type Brand")]
            [RegularExpression("[a-zA-Z]{3,30}")]
            public string Brand { get; set; }
            [Required(ErrorMessage = "Please type Model")]
            public string Model { get; set; }
            [Required(ErrorMessage = "Please type mileage")]
            [Range(0,1500000)]
            public decimal Mileage { get; set; }
            [Required(ErrorMessage = "Please type ReleaseDate")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime ReleaseDate { get; set; }

            public int BodyTypeId { get; set; }
            [Required(ErrorMessage = "Please type count")]
            [Range(typeof(int),"0", "100")]
            public int Count { get; set; }
            [Required(ErrorMessage = "Please type ReleaseDate")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime AddDate { get; set; }
            
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.DateTime> ModifiedDate { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Borrow> Borrow { get; set; }
            public virtual CarBodyType CarBodyType { get; set; }

           

           
        }



        [MetadataType(typeof(BorrowMetadata))]
        public partial class Borrow
        {
        }
        public partial class BorrowMetadata
        {
            [Required]
            public int UserId { get; set; }
            [Required]
            public int CarId { get; set; }
          
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime FromDate { get; set; }
            
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            
            public Nullable<System.DateTime> ToDate { get; set; }
            public bool IsReturned { get; set; }

            public virtual Car Car { get; set; }
            public virtual User User { get; set; }
        }


    }
