using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone_ECommerce_progject.Models;

namespace Capstone_ECommerce_progject.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        Store storeDB = new Store();
        const string PromoCode = "FREE";

        // GET: Checkout //Address and payment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //Post / Checkout / AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if(string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.UserName = User.Identity.Name;
                    order.orderDate = DateTime.Now;

                    //save order
                    storeDB.Order.Add(order);
                    storeDB.SaveChanges();

                    //process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                //Display With error
                return View(order);
            }
        }
        //Checkout/Complete
        public ActionResult Complete(int id)
        {
            //Validate customer owns this order
            bool isValid = storeDB.Order.Any(o => o.OrderId == id && o.UserName == User.Identity.Name);

            if(isValid)
            {
                ViewBag.Message = id.ToString();
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}