using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone_ECommerce_progject.Models;
using Capstone_ECommerce_progject.View_Models;

namespace Capstone_ECommerce_progject.Controllers
{
    public class ShoppingCartController : Controller
    {
        Store storeDB = new Store();


        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);

        }

        //GET: Store/AddToCart
        public ActionResult AddToCart(int id)
        {

            //retrieve the album from the database
            var addedAlbum = storeDB.Albums.Single(
                album => album.AlbumID == id);

            //add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            //go back to the main page for more shopping
            return RedirectToAction("Index");
        }
        // Ajax: /ShoppingCart/ Update Cart count
        [HttpPost]
        public ActionResult UpdateCartCount(int id, int cartCount)
        {
            ShoppingCartRemoveViewModel results = null;
            try
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);

                //get the name of the album to display confirmation
                string albumName = storeDB.Cart
                    .Single(item => item.RecordID == id).Album.Title;

                //Update cart count
                int itemCount = cart.UpdateCartCount(id, cartCount);

                //prepare message
                string msg = "the quantit of " + Server.HtmlEncode(albumName) +
                    " has been refreshed in your shopping cart.";
                if (itemCount == 0) msg = Server.HtmlEncode(albumName) +
                         " has been removed from your shopping cart.";

                //Display the confirmation message
                results = new ShoppingCartRemoveViewModel
                {
                    Message = msg,
                    CartTotal = cart.GetTotal(),
                    CartCount = cart.GetCount(),
                    ItemCount = itemCount,
                    DeleteID = id
                };
            }
            catch
            {
                results = new ShoppingCartRemoveViewModel
                {
                    Message = "Error occurred or invalid input...",
                    CartTotal = -1,
                    CartCount = -1,
                    ItemCount = -1,
                    DeleteID = id
                };
            }
            return Json(results);
        }


        //Ajax Remove from Shopping Cart
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            ShoppingCartRemoveViewModel results = null;
                
            //remove item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //get the name of the albums to display confirmation
            string albumName = storeDB.Cart.Single(item => item.RecordID == id).Album.Title;
        

            //remove from cart
            int itemCount = cart.RemoveFromCart(id);

            //Display the confirmation message
            results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                "has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteID = id
            };
            return Json(results);
        }
        //GET Shoppingcart/Summary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

    }
}