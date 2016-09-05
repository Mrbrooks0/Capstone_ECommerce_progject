using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Capstone_ECommerce_progject.Models
{
    [Bind(Exclude = "OrderId")]
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime orderDate { get; set; }
        [ScaffoldColumn(false)]
        public string UserName { get; set; }
        [Required(ErrorMessage ="First Name is Required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City  is Required")]
        [DisplayName("City")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is Required")]
        [DisplayName("State")]
        public string State { get; set; }
        [Required(ErrorMessage = "State is Required")]
        [DisplayName("Zip")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Country is Required")]
        [DisplayName("Country")]
        public string Country { get; set; }
        
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
           ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        

        public List<Order_Details> OrderDetails { get; set; }
    }
}