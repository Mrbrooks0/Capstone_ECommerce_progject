using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone_ECommerce_progject.Models;

namespace Capstone_ECommerce_progject.View_Models
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteID { get; set; }
    }
}