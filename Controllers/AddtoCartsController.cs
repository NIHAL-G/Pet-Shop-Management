using System;

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using System.Web;

using System.Web.Mvc; 

using Shop.Models;


namespace Shop.Controllers
{

    //[Authorize]
    public class AddtoCartsController : Controller

    {

        WebsiteEntities db = new WebsiteEntities();

        // GET: MyCart


        public ActionResult CartView()

        {
            

            List<AddtoCart> cart = Session["Cart"] as List<AddtoCart>;

            if (cart == null)

            {

                cart = new List<AddtoCart>();

                Session["Cart"] = cart;

            }

            return View(cart);

        }

        public ActionResult PaidView()
        {
            return View();
        }




        // GET: Products/AddToCart

        [HttpPost]

        public ActionResult AddToCart(int id, string name, int price, int quantity = 1)

        {

            List<AddtoCart> cart = Session["Cart"] as List<AddtoCart> ?? new List<AddtoCart>();

            AddtoCart existingItem = cart.FirstOrDefault(item => item.Id == id);

            if (existingItem != null)

            {

                existingItem.Quantity += quantity;

            }

            else

            {

                cart.Add(new AddtoCart { Id = id, Price = price, Name = name, Quantity = quantity });

            }

            Session["Cart"] = cart;

            return RedirectToAction("CartView");

        }

        public ActionResult RemoveFromCart(int id)

        {

            List<AddtoCart> cart = Session["Cart"] as List<AddtoCart>;

            if (cart != null)

            {

                cart.RemoveAll(item => item.Id == id);

                Session["Cart"] = cart;

            }

            return RedirectToAction("CartView");
        }

        public int GetCurrentUserId()
        {
            return 1;
        }
        
        public ActionResult Checkout()

        {

            // Retrieve cart items from session

            List<AddtoCart> cartItems = Session["Cart"] as List<AddtoCart>;

            if (cartItems == null || cartItems.Count == 0)

            {

                // Redirect to an empty cart page or display a message

                return RedirectToAction("EmptyCart");

            }

            // Calculate total amount

            int price = (int)cartItems.Sum(item => item.Price * item.Quantity);

            // Create a new order

            var buy = new Buy

            {

                Id = GetCurrentUserId(), // Replace with actual logic to get the current user ID

                Price = price,

                BuyDate = DateTime.Now,


                 // Set the initial order status

            };

            // Save the order to the database

            db.Buys.Add(buy);

            db.SaveChanges();

            // Optionally, you can clear the cart session after creating the order

            Session["Cart"] = null;

            // Redirect to the order confirmation page with the order ID

            return RedirectToAction("PaidView", new { orderId = buy.Id }); 

        }
        public ActionResult ClearCart()

        {


            Session.Remove("Cart");

            return Redirect("~/Pets/UserView");

        }
    }

}