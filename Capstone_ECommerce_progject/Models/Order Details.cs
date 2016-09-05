using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone_ECommerce_progject.Models
{
    public class Order_Details
    {
        public int Order_DetailsID { get; set; }
        public int OrderId { get; set; }
        public int AlbumID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Album Album { get; set; }
        public virtual Order Order { get; set; }
    }
}