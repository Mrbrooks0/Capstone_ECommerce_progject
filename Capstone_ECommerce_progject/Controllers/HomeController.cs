using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone_ECommerce_progject.Models;

namespace Capstone_ECommerce_progject.Controllers
{
    public class HomeController : Controller
    {
        private Store storeDB = new Store();

        private List<Album> GetTopSellingAlbums(int count)

        {
            //Grou order details by album and reutn
            //The albums with the highestcount
            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
            
        }
        public ActionResult Index()
        {
            //get most popular items
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}