using LibraryManagementSystem.Libs.Utilities;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class ApplyController : Controller
    {
        // GET: Apply
        public ActionResult Index()
        {
            LibraryDbContext db = new LibraryDbContext();
            var oTblUsers = (TblUser)Session["TblUsers"];
            var listApplicant = oTblUsers.UserType == UserType.Admin.ToString() ? db.Applicants.ToList() : db.Applicants.Where(o => o.UserID == oTblUsers.UserID).ToList();
            return View(listApplicant);
        }

        public ActionResult Edit(Int64 id)
        {
            LibraryDbContext db = new LibraryDbContext();
            var oApplicant = (from o in db.Applicants where o.ApplicantId == id select o).FirstOrDefault();
            return View(oApplicant);
        }

        [HttpPost]
        public ActionResult Edit(Applicant oApplicant, List<Member> listEdu)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                var eduS = (from o in db.Members where o.ApplicantId == oApplicant.ApplicantId select o).ToList();
                foreach (var item in eduS)
                {
                    db.Members.Remove(item);
                }

                db.SaveChanges();
            }

            using (LibraryDbContext db = new LibraryDbContext())
            {
                //Check for NULL.
                if (listEdu == null)
                {
                    listEdu = new List<Member>();
                }

                //Loop and insert records.
                foreach (var item in listEdu)
                {
                    item.ApplicantId = oApplicant.ApplicantId;
                    db.Members.Add(item);
                }

                //Loop and insert records.

                var objApplicant = (from o in db.Applicants where o.ApplicantId == oApplicant.ApplicantId select o).FirstOrDefault();
                objApplicant.EmailAddr = oApplicant.EmailAddr;
                objApplicant.Phone = oApplicant.Phone;
                objApplicant.ApplicantName = oApplicant.ApplicantName;

                db.SaveChanges();

                return Json(new { resState = true });
            }
        }

        // register
        [HttpGet]
        public ActionResult Create()
        {
            return View(new TblUser());
        }

        // register submit
        [HttpPost]
        public ActionResult Create(TblUser model)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                model.UserType = UserType.Applicant.ToString();
                db.TblUser.Add(model);
                db.SaveChanges();
            }

            using (LibraryDbContext db = new LibraryDbContext())
            {
                var oApplicant = new Applicant();
                oApplicant.ApplicantName = model.Username;
                oApplicant.UserID = model.UserID;
                db.Applicants.Add(oApplicant);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
