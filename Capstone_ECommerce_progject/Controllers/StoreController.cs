using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone_ECommerce_progject.Models;

namespace Capstone_ECommerce_progject.Controllers
{
    public class StoreController : Controller
    {
        Store storeDB = new Store();

        //Get /store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();
            return PartialView(genres);
        }
        // GET: Store
        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();
           
            return View(genres);
        }
        //Browse products by Genre
        public ActionResult Browse(string genre)
        {
            var genreModel = storeDB.Genres.Include("Albums")
                .Single(g => g.Name == genre);
           
            return View(genreModel);
        }
        //get item details
        public ActionResult Details(int id)
        {
            var album = storeDB.Albums.Find(id);
            return View(album);
        }
    }
}