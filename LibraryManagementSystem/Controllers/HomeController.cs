using LibraryManagementSystem.Libs.Utilities;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
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
        //User Role Permission
        #region Login
        private LibraryDbContext db = new LibraryDbContext();
        public ActionResult Login()
        {
            return View();
        }

        // POST: HomeController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,UserPass")] TblUser model)
        {
            try
            {
                var oTblUser = db.TblUser.Where(o => o.Username == model.Username && o.UserPass == model.UserPass).FirstOrDefault();
                if (oTblUser != null)
                {
                    var listTblUserRole = db.TblUserRole.Where(o => o.UserID == oTblUser.UserID).ToList();
                    Session["TblUsers"] = oTblUser;
                    Session["TblUserRoles"] = listTblUserRole;
                    if (oTblUser.UserType == UserType.SuperAdmin.ToString())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (oTblUser.UserType == UserType.GeneralUser.ToString())
                    {
                        return RedirectToAction("Index", "Books");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("TblUsers");
            return RedirectToAction("Index", "Home");
        }
        #endregion

        //Member Profile

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View(new TblUser());
        }

        [HttpPost]
        public ActionResult UserLogin(TblUser model)
        {
            try
            {
                LibraryDbContext db = new LibraryDbContext();
                var oTblUsers = db.TblUser.Where(o => o.Username == model.Username && o.UserPass == model.UserPass).FirstOrDefault();
                if (oTblUsers != null)
                {
                    //var listTblUserRole = db.TblUserRoles.Where(o => o.UserID == oTblUser.UserID).ToList();
                    Session["TblUsers"] = oTblUsers;
                    //Session["TblUserRoles"] = listTblUserRole;
                    //if (oTblUser.UserType == UserType.SuperAdmin.ToString())
                    //{
                    //    return RedirectToAction("Index", "Users");
                    //}
                    //else if (oTblUser.UserType == UserType.GeneralUser.ToString())
                    //{
                    //    return RedirectToAction("Index", "Products");
                    //}
                    return RedirectToAction("Index", "Apply");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UserLogout()
        {
            Session.Remove("TblUsers");
            return RedirectToAction("Index", "Home");
        }
        //Search
        public ActionResult Search()
        {
            
            return View(new List<Book>());
        }

        [HttpPost]
        public ActionResult Search(string Name)
        {
            //var slf = new Shelf();
            Name = Name.Trim();
            var obook = db.Books.Where(t => t.book_name.StartsWith(Name)).ToList();
            return View(obook);
        }
    }
}