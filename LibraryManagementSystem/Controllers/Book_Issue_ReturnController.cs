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
    public class Book_Issue_ReturnController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Book_Issue_Return
        public ActionResult Index()
        {
            var book_Issue_Return = db.Book_Issue_Return.Include(b => b.Book).Include(b => b.Member);
            return View(book_Issue_Return.ToList());
        }

        // GET: Book_Issue_Return/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Issue_Return book_Issue_Return = db.Book_Issue_Return.Find(id);
            if (book_Issue_Return == null)
            {
                return HttpNotFound();
            }
            return View(book_Issue_Return);
        }

        // GET: Book_Issue_Return/Create
        public ActionResult Create()
        {
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name");
            ViewBag.member_id = new SelectList(db.Members, "member_id", "member_name");
            return View();
        }

        // POST: Book_Issue_Return/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "issue_id,book_id,book_details_id,member_id,issue_date,due_date,return_date, quantity,status")] Book_Issue_Return book_Issue_Return)
        {
            //Stock Out
            if (ModelState.IsValid)
            {
                Book_Issue_Return oReceive = new Book_Issue_Return();
                db.Book_Issue_Return.Add(book_Issue_Return);
                var oStock = (from o in db.Book_Stock_Master where o.book_id == book_Issue_Return.book_id select o).FirstOrDefault();
                if (oStock == null)
                {
                    oStock = new Book_Stock_Master();
                    oStock.book_id = book_Issue_Return.book_id;
                    oStock.quantity = book_Issue_Return.quantity;
                    oStock.status = "Stock Out";
                    db.Book_Stock_Master.Add(oStock);
                }

                else
                {
                    oStock.quantity -= book_Issue_Return.quantity;
                    oStock.status = "Stock Out";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_Issue_Return);

         

        }
        [HttpGet]
        public ActionResult BookReturn()
        {
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name");
            ViewBag.member_id = new SelectList(db.Members, "member_id", "member_name");
            return View();
        }

        // POST: BookReturn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookReturn([Bind(Include = "issue_id,book_id,book_details_id,member_id,issue_date,due_date,return_date, quantity,status")] Book_Issue_Return book_Issue_Return)
        {
            //Stock Out
            if (ModelState.IsValid)
            {
                Book_Issue_Return oReceives = new Book_Issue_Return();
                db.Book_Issue_Return.Add(book_Issue_Return);
                var oStocks = (from o in db.Book_Stock_Master where o.book_id == book_Issue_Return.book_id select o).FirstOrDefault();
                if (oStocks == null)
                {
                    oStocks = new Book_Stock_Master();
                    oStocks.book_id = book_Issue_Return.book_id;
                    oStocks.quantity = book_Issue_Return.quantity;
                    oStocks.status = "Book Return";
                    db.Book_Stock_Master.Add(oStocks);
                }

                else
                {
                    oStocks.quantity += book_Issue_Return.quantity;
                    oStocks.status = "Book Return";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_Issue_Return);

        }

        // GET: Book_Issue_Return/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Issue_Return book_Issue_Return = db.Book_Issue_Return.Find(id);
            if (book_Issue_Return == null)
            {
                return HttpNotFound();
            }
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name", book_Issue_Return.book_id);
            ViewBag.member_id = new SelectList(db.Members, "member_id", "member_name", book_Issue_Return.member_id);
            return View(book_Issue_Return);
        }

        // POST: Book_Issue_Return/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "issue_id,book_id,book_details_id,member_id,issue_date,due_date,return_date,status")] Book_Issue_Return book_Issue_Return)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_Issue_Return).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name", book_Issue_Return.book_id);
            ViewBag.member_id = new SelectList(db.Members, "member_id", "member_name", book_Issue_Return.member_id);
            return View(book_Issue_Return);
        }

        // GET: Book_Issue_Return/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Issue_Return book_Issue_Return = db.Book_Issue_Return.Find(id);
            if (book_Issue_Return == null)
            {
                return HttpNotFound();
            }
            return View(book_Issue_Return);
        }

        // POST: Book_Issue_Return/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_Issue_Return book_Issue_Return = db.Book_Issue_Return.Find(id);
            db.Book_Issue_Return.Remove(book_Issue_Return);
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
