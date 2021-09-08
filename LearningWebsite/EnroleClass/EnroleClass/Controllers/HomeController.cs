using EnroleClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnroleClass.Controllers
{
    public class HomeController : Controller
    {
        Entities db = new Entities();

        public ActionResult Index()
        {
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var objUser = db.Users.Where(q => q.UserEmail == user.UserEmail && q.UserPassword == user.UserPassword).FirstOrDefault();
            if (objUser != null)
            {
                HttpContext.Session["userID"] = objUser.UserId;
                HttpContext.Session["userEmail"] = objUser.UserEmail;
                FormsAuthentication.SetAuthCookie(objUser.UserEmail, false);
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            var count = db.Users.Where(q => q.UserEmail == user.UserEmail && q.UserPassword == user.UserPassword).Count();
            if (count > 0)
            {
                return View();
            }
            else if (user.UserPassword != user.ConfirmPassword)
            {
                return View();
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");

            }
        }


        public ActionResult ClassList()
        {
            Entities db = new Entities();
            List<Class> listClasses = db.Classes.ToList();
            return View(listClasses);
        }
        public ActionResult EnrollInClass()
        {
            if (HttpContext.Session["userId"] != null)
            {
                CustomClasses customClasses = new CustomClasses();
                customClasses.ClassList = db.Classes;
                return View(customClasses);
            }
            else
                return RedirectToAction("Login");

        }
        [HttpPost]
        public ActionResult EnrollInClass(CustomClasses cls)
        {
            if (HttpContext.Session["userId"] != null)
            {
                int selectedID = cls.ClassId;
                int UserID = Convert.ToInt32(HttpContext.Session["userId"].ToString());
                UserClass userClass = new UserClass()
                {
                    ClassId = selectedID,
                    UserId = UserID
                };
                db.UserClasses.Add(userClass);
                db.SaveChanges();
                return RedirectToAction("StudentClasses");
            }
            else
                return RedirectToAction("Login");

        }

        public ActionResult StudentClasses()
        {
            if (HttpContext.Session["userId"] != null)
            {

                int UserID = Convert.ToInt32(HttpContext.Session["userId"].ToString());
                List<Class> listClass = (from cl in db.Classes
                                         join usc in db.UserClasses on cl.ClassId equals usc.ClassId
                                         where usc.UserId == UserID
                                         select cl).ToList();
                return View(listClass);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {

            HttpContext.Session["userID"] = null;
            HttpContext.Session["userEmail"] = null;
            return RedirectToAction("Login");

        }
    }
}