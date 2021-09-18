using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using PagedList;

namespace LibraryManagementSystem.Controllers
{
    public class Book_Stock_MasterController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Book_Stock_Master
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var books = from s in db.Book_Stock_Master
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s =>
               s.status.ToUpper().Contains(searchString.ToUpper())
                ||
               s.status.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.status);
                    break;
                //case "Date":
                //    books = books.OrderBy(s => s.EnrollmentDate);
                //    break;
                //case "date_desc":
                //    books = books.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    books = books.OrderBy(s => s.status);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));







            //return View(db.Book_Stock_Master.ToList());
        }

        // GET: Book_Stock_Master/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Stock_Master book_Stock_Master = db.Book_Stock_Master.Find(id);
            if (book_Stock_Master == null)
            {
                return HttpNotFound();
            }
            return View(book_Stock_Master);
        }

        // GET: Book_Stock_Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book_Stock_Master/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stock_master_id,quantity,publisher_version,book_id,status")] Book_Stock_Master book_Stock_Master)
        {
            if (ModelState.IsValid)
            {
                db.Book_Stock_Master.Add(book_Stock_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_Stock_Master);
        }

        // GET: Book_Stock_Master/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Stock_Master book_Stock_Master = db.Book_Stock_Master.Find(id);
            if (book_Stock_Master == null)
            {
                return HttpNotFound();
            }
            return View(book_Stock_Master);
        }

        // POST: Book_Stock_Master/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stock_master_id,quantity,publisher_version,book_id,status")] Book_Stock_Master book_Stock_Master)
        {
            if (ModelState.IsValid)
            {
                //Stock Add

                //    Book_Stock_Master oReceive = new Book_Stock_Master();
                //    db.Book_Stock_Master.Add(book_Stock_Master);
                //    var oStock = (from o in db.Book_Stock_Master where o.book_id == book_Stock_Master.book_id select o).FirstOrDefault();
                //    if (oStock == null)
                //    {
                //        oStock = new Book_Stock_Master();
                //        oStock.book_id = book_Stock_Master.book_id;
                //        oStock.quantity = book_Stock_Master.quantity;
                //        oStock.status = "Stock Out";
                //        //db.Book_Stock_Master.Add(oStock);
                //        db.Entry(book_Stock_Master).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        oStock.quantity += book_Stock_Master.quantity;
                //        oStock.status = "Stock In";
                //    }

                //    return RedirectToAction("Index");
                //}

                //return View(book_Stock_Master);






                db.Entry(book_Stock_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book_Stock_Master);
        }

        // GET: Book_Stock_Master/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Stock_Master book_Stock_Master = db.Book_Stock_Master.Find(id);
            if (book_Stock_Master == null)
            {
                return HttpNotFound();
            }
            return View(book_Stock_Master);
        }

        // POST: Book_Stock_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_Stock_Master book_Stock_Master = db.Book_Stock_Master.Find(id);
            db.Book_Stock_Master.Remove(book_Stock_Master);
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
