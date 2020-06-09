using Microsoft.AspNet.Identity;
using MrSimonAcademy2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrSimonAcademy2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //var userId = User.Identity.GetUserId();
            //var user = new User();
            //using (UserContext db = new UserContext())
            //{
            //    user = db.Users.Find(userId);
            //}
      
            return View();
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

        public ActionResult Services()
        {
            ViewBag.Message = "Your services page.";

            return View();
        }

        //[Authorize(Roles = "admin")]
        public ActionResult GetUsers()
        {
            // IndexViewModel отображает пользователя
            List<User> users = new List<User>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        public ActionResult AddUser()
        {
            return RedirectToActionPermanent("Register", "Account", new RegisterModel());
            //return Redirect("/Account/Register");
        }
    }
}