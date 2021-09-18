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
    public class PurchasesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Purchases
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var purchase = db.Purchase.Include(p => p.Book);
            //Paigination
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

            var purchases = from s in db.Purchase
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                purchase = purchase.Where(s =>
               s.description.ToUpper().Contains(searchString.ToUpper())
                ||
               s.description.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    purchases = purchases.OrderByDescending(s => s.description);
                    break;
                case "Date":
                    purchases = purchases.OrderBy(s => s.date);
                    break;
                case "date_desc":
                    purchases = purchases.OrderByDescending(s => s.date);
                    break;
                default:
                    purchases = purchases.OrderBy(s => s.description);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(purchases.ToPagedList(pageNumber, pageSize));
            //return View(purchase.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchase.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name");
            return View();
        }

        // POST: Purchases/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchase_id,book_id,date,quantity,description,publisher_version")] Purchase purchase)
        {
            //stock in

            if (ModelState.IsValid)
            {
                Purchase oReceive = new Purchase();
                db.Purchase.Add(purchase);
                var oStock = (from o in db.Book_Stock_Master where o.book_id == purchase.book_id select o).FirstOrDefault();
                if (oStock == null)
                {
                    oStock = new Book_Stock_Master();
                    oStock.book_id = purchase.book_id;
                    oStock.quantity = purchase.quantity;
                    oStock.publisher_version = purchase.publisher_version;
                    oStock.status = "Stock in";
                    db.Book_Stock_Master.Add(oStock);
                }
                else
                {
                    oStock.quantity += purchase.quantity;
                    oStock.status = "Stock in";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchase);

        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchase.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name", purchase.book_id);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "purchase_id,book_id,date,quantity,description,publisher_version")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                Purchase oReceive = new Purchase();
                db.Purchase.Add(purchase);
                var oStock = (from o in db.Book_Stock_Master where o.book_id == purchase.book_id select o).FirstOrDefault();
                if (oStock == null)
                {
                    oStock = new Book_Stock_Master();
                    oStock.book_id = purchase.book_id;
                    oStock.quantity = purchase.quantity;
                    oStock.publisher_version = purchase.publisher_version;
                    oStock.status = "Stock in";
                    db.Entry(purchase).State = EntityState.Modified;
                }
                else
                {
                    oStock.quantity += purchase.quantity;
                    oStock.status = "Stock in";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name", purchase.book_id);
            return View(purchase);

        }



        //    db.Entry(purchase).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //ViewBag.book_id = new SelectList(db.Books, "book_id", "book_name", purchase.book_id);
        //return View(purchase);
        //}

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchase.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchase.Find(id);
            db.Purchase.Remove(purchase);
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
