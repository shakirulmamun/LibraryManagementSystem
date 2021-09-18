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
    public class Fine_RulesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Fine_Rules
        public ActionResult Index()
        {
            return View(db.Fine_Rules.ToList());
        }

        // GET: Fine_Rules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine_Rules fine_Rules = db.Fine_Rules.Find(id);
            if (fine_Rules == null)
            {
                return HttpNotFound();
            }
            return View(fine_Rules);
        }

        // GET: Fine_Rules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fine_Rules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fine_id,default_duration,max_duration,duration_unit,fine_charge")] Fine_Rules fine_Rules)
        {
            if (ModelState.IsValid)
            {
                db.Fine_Rules.Add(fine_Rules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fine_Rules);
        }

        // GET: Fine_Rules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine_Rules fine_Rules = db.Fine_Rules.Find(id);
            if (fine_Rules == null)
            {
                return HttpNotFound();
            }
            return View(fine_Rules);
        }

        // POST: Fine_Rules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fine_id,default_duration,max_duration,duration_unit,fine_charge")] Fine_Rules fine_Rules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fine_Rules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fine_Rules);
        }

        // GET: Fine_Rules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine_Rules fine_Rules = db.Fine_Rules.Find(id);
            if (fine_Rules == null)
            {
                return HttpNotFound();
            }
            return View(fine_Rules);
        }

        // POST: Fine_Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fine_Rules fine_Rules = db.Fine_Rules.Find(id);
            db.Fine_Rules.Remove(fine_Rules);
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
