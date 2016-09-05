using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone_ECommerce_progject.Models;

namespace Capstone_ECommerce_progject.View_Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}