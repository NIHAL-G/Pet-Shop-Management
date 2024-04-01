using Shop.Models;
using Shop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace RegistrationLogin.Controllers
{
    public class BuyController : Controller
    {
        private WebsiteEntities db = new WebsiteEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // Action to display order history for the current user
        public ActionResult OrdersList()
        {
            
            return View(db.Buys.ToList());
        }
    }
}
