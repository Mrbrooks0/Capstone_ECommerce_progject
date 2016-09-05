using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Capstone_ECommerce_progject.Models
{
    public class Store : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Artist> Artist { get; set; }
        public DbSet<Cart> Cart{ get; set; }
        public DbSet<Order> Order{ get; set; }
        public DbSet<Order_Details> Order_Details { get; set; }

        //public System.Data.Entity.DbSet<Capstone_ECommerce_progject.Models.Artist> Artists { get; set; }

        public System.Data.Entity.DbSet<Capstone_ECommerce_progject.Models.Roles> Roles { get; set; }
    }
}