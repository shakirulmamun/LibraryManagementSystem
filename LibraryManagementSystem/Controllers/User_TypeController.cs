using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    public class User_TypeController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: User_Type
        public ActionResult Index()
        {
            return View(db.User_Type.ToList());
        }

        // GET: User_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Type user_Type = db.User_Type.Find(id);
            if (user_Type == null)
            {
                return HttpNotFound();
            }
            return View(user_Type);
        }

        // GET: User_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,type")] User_Type user_Type)
        {
            if (ModelState.IsValid)
            {
                db.User_Type.Add(user_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Type);
        }

        // GET: User_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Type user_Type = db.User_Type.Find(id);
            if (user_Type == null)
            {
                return HttpNotFound();
            }
            return View(user_Type);
        }

        // POST: User_Type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,type")] User_Type user_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Type);
        }

        // GET: User_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Type user_Type = db.User_Type.Find(id);
            if (user_Type == null)
            {
                return HttpNotFound();
            }
            return View(user_Type);
        }

        // POST: User_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Type user_Type = db.User_Type.Find(id);
            db.User_Type.Remove(user_Type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
