using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone_ECommerce_progject.Models
{
    public partial class ShoppingCart
    {
        Store storeDB = new Store();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartID(context);
            return cart;
        }

        //helper method to cart call
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Album album)
        {
            //get the matching cart and album instances
            var cartItem = storeDB.Cart.SingleOrDefault(c => c.CartID == ShoppingCartId
            && c.AlbumID == album.AlbumID);

            if(cartItem == null)
            {
                //creat a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumID = album.AlbumID,
                    CartID = ShoppingCartId,
                    count = 1,
                    DateCreated = DateTime.Now
                };

                storeDB.Cart.Add(cartItem);
            }
            else
            {
                //if the cart item exists in the cart add to the quantity
                cartItem.count++;
            }
            //save canges
            storeDB.SaveChanges();
        }

        //Update Cart count
        public int UpdateCartCount(int id, int cartCount)
        {
            //get the cart
            var cartItem = storeDB.Cart.Single(
                cart => cart.CartID == ShoppingCartId 
                && cart.RecordID == id);

            int itemCount = 0;

            if(cartItem != null)
            {
                if(cartCount > 0)
                {
                    cartItem.count = cartCount;
                    itemCount = cartItem.count;
                }
                else
                {
                    storeDB.Cart.Remove(cartItem);
                }
                storeDB.SaveChanges();
            }
            return itemCount;
        }


        public int RemoveFromCart(int id)
        {
            //get the cart
            var cartItem = storeDB.Cart.SingleOrDefault(c => c.CartID == ShoppingCartId
            && c.RecordID == id);

            int itemCount = 0;

            if(cartItem != null)
            {
                storeDB.Cart.Remove(cartItem);
                               
            }
            //save changes
            storeDB.SaveChanges();
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Cart.Where(cart => cart.CartID == ShoppingCartId);
            foreach(var cartItem in cartItems)
            {
                storeDB.Cart.Remove(cartItem);
            }
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Cart.Where(cart => cart.CartID == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Cart
                          where cartItems.CartID == ShoppingCartId
                          select
                          (int?)cartItems.count).Sum();

            //return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            //Multiply price by the count of the album
            //uses the price currently in the cart
            //sum all the price totals to get the cart total

            decimal? total = (from cartItems in storeDB.Cart
                              where cartItems.CartID == ShoppingCartId
                              select (int?)cartItems.count * cartItems.Album.Price).Sum();
            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();

            //Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetails = new Order_Details
                {
                    AlbumID = item.AlbumID,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.count
                };

                //set the order total of the shopping cart
                orderTotal += (item.count * item.Album.Price);
                storeDB.Order_Details.Add(orderDetails);
            }
            //set the orders total to the orderTotal count
            order.Total = orderTotal;
            //save the order
            storeDB.SaveChanges();
            //empty the shopping cart
            EmptyCart();
            //return the orderID as the confirmation number
            return order.OrderId;
        }
        //access cookies using httpContextBase
        public string GetCartID(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else //Generate a new random GUID using system Guid class
                {
                    Guid tempCartID = Guid.NewGuid();

                    //send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartID.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        //when a user has logged in, migrate their shopping cart to tbe associated with their username
        public void MigrateCart(string username)
        {
            var shoppingCart = storeDB.Cart.Where(c => c.CartID == ShoppingCartId);

            foreach(Cart item in shoppingCart)
            {
                item.CartID = username;
            }
            storeDB.SaveChanges();
        }
        
    }
}