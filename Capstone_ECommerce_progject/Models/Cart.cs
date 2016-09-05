using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone_ECommerce_progject.Models
{
    public class Cart
    {
        [Key]
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int AlbumID { get; set; }
        public int count { get; set; }
        public System.DateTime DateCreated { get; set; }

        public virtual Album Album { get; set; }

    }
}