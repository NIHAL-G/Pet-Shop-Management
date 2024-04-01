using Shop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        public static bool isLoggedIn = false;
        public static bool isAdmin = false;
        public static bool isUser = false;

        WebsiteEntities db = new WebsiteEntities();
        // GET: Account
        public ActionResult Index()
        {
            if (isLoggedIn == false || isAdmin == false)
                return HttpNotFound();

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegister ur)
        {
            if(ModelState.IsValid)
            {
                if(db.UserRegisters.Any(x => x.Email == ur.Email))
                {
                    ViewBag.Message = "Email already registered";
                }
                else
                {
                    db.UserRegisters.Add(ur);
                    db.SaveChanges();
                    Response.Write("<script>alert('Registration Successful')</script>");
                }
            }
            return View();  
        }
        [HttpGet]
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(MyLogin l)
        {
            
           
            var user = db.UserRegisters.SingleOrDefault(m => m.Email == l.Email && m.password == l.Password);

            var admin = db.AdminRegisters.SingleOrDefault(m => m.Email == l.Email && m.password == l.Password);

            if (user != null)

            {

                Session["IsLoggedIn"] = true;

                Session["IsUser"] = true;

                Session["UserName"] = user.UserName;

                return RedirectToAction("UserView", "Pets");

            }

            else if (admin != null)

            {

                Session["IsLoggedIn"] = true;

                Session["IsAdmin"] = true;

                Session["UserName"] = admin.AdminName;

                return RedirectToAction("Index", "Pets");

            }

            else

            {

                ViewBag.ErrorMessage = "Invalid Credentials";

                return View();

            }

        
    }
        public ActionResult Logout()
        {
            Session["IsLoggedIn"] = false;
            Session["IsUser"] = false;
            Session["IsAdmin"] = false;

            return RedirectToAction("Index", "Home");
        }
       
    }
}