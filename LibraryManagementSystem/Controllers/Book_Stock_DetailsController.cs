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
    public class Book_Stock_DetailsController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Book_Stock_Details
        public ActionResult Index()
        {
            var book_Stock_Details = db.Book_Stock_Details.Include(b => b.Book_Stock_Master).Include(b => b.Shelf);
            return View(book_Stock_Details.ToList());
        }

        // GET: Book_Stock_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Stock_Details book_Stock_Details = db.Book_Stock_Details.Find(id);
            if (book_Stock_Details == null)
            {
                return HttpNotFound();
            }
            return View(book_Stock_Details);
        }

        // GET: Book_Stock_Details/Create
        public ActionResult Create()
        {
            ViewBag.stock_master_id = new SelectList(db.Book_Stock_Master, "stock_master_id", "stock_master_id");
            ViewBag.shelf_id = new SelectList(db.Shelves, "shelf_id", "shelf_name");
            return View();
        }

        // POST: Book_Stock_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stock_details_id,stock_master_id,book_serial_number,shelf_id,rack_number,status")] Book_Stock_Details book_Stock_Details)
        {
            if (ModelState.IsValid)
            {
                db.Book_Stock_Details.Add(book_Stock_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stock_master_id = new SelectList(db.Book_Stock_Master, "stock_master_id", "stock_master_id", book_Stock_Details.stock_master_id);
            ViewBag.shelf_id = new SelectList(db.Shelves, "shelf_id", "shelf_name", book_Stock_Details.shelf_id);
            return View(book_Stock_Details);
        }

        // GET: Book_Stock_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Stock_Details book_Stock_Details = db.Book_Stock_Details.Find(id);
            if (book_Stock_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.stock_master_id = new SelectList(db.Book_Stock_Master, "stock_master_id", "stock_master_id", book_Stock_Details.stock_master_id);
            ViewBag.shelf_id = new SelectList(db.Shelves, "shelf_id", "shelf_name", book_Stock_Details.shelf_id);
            return View(book_Stock_Details);
        }

        // POST: Book_Stock_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stock_details_id,stock_master_id,book_serial_number,shelf_id,rack_number,status")] Book_Stock_Details book_Stock_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_Stock_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stock_master_id = new SelectList(db.Book_Stock_Master, "stock_master_id", "stock_master_id", book_Stock_Details.stock_master_id);
            ViewBag.shelf_id = new SelectList(db.Shelves, "shelf_id", "shelf_name", book_Stock_Details.shelf_id);
            return View(book_Stock_Details);
        }

        // GET: Book_Stock_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Stock_Details book_Stock_Details = db.Book_Stock_Details.Find(id);
            if (book_Stock_Details == null)
            {
                return HttpNotFound();
            }
            return View(book_Stock_Details);
        }

        // POST: Book_Stock_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_Stock_Details book_Stock_Details = db.Book_Stock_Details.Find(id);
            db.Book_Stock_Details.Remove(book_Stock_Details);
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
