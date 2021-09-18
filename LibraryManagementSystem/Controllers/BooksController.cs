using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;

using PagedList;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Books
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var bookss = db.Books.Include(b => b.Author).Include(b => b.Category);
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

            var books = from s in db.Books
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s =>
               s.book_name.ToUpper().Contains(searchString.ToUpper())
                ||
               s.description.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.book_name);
                    break;
                //case "Date":
                //    books = books.OrderBy(s => s.EnrollmentDate);
                //    break;
                //case "date_desc":
                //    books = books.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    books = books.OrderBy(s => s.book_name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));


            //var bookss = db.Books.Include(b => b.Author).Include(b => b.Category);
            //return View(bookss.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {

            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = " book_id,author_id,category_id,book_name,cover_image,File,description,Quantity")] Book book)
        {

            //Paigination

            if (ModelState.IsValid)
            {
                //Role Permission
                var bookId = db.Books.Max(o => (int?)o.book_id) ?? 0;
                var oTblbook = new Book();
                oTblbook.book_id = bookId + 1;
                oTblbook.book_name = book.book_name;
                oTblbook.Quantity = book.Quantity;
                //db.Books.Add(oTblbook); //
                db.SaveChanges();


                //Image
                string filename = Path.GetFileName(book.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                book.cover_image = "~/Images/" + _filename;
                db.Books.Add(book);
                if (book.File.ContentLength < 1000000)
                {
                    if (db.SaveChanges() > 0)
                    {
                        book.File.SaveAs(path);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "File must less then or Equal to 1 MB";
                }
            }
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name", book.author_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", book.category_id);
            return View(book);

        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            Session["imgPath"] = book.cover_image;
            if (book == null)
            {
                return HttpNotFound();
            }

            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name", book.author_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", book.category_id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_id,author_id,category_id,book_name,cover_image,File,description,Quantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.File != null)
                {
                    string filename = Path.GetFileName(book.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    book.cover_image = "~/Images/" + _filename;

                    if (book.File.ContentLength < 1000000)
                    {
                        db.Entry(book).State = EntityState.Modified;
                        string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                        if (db.SaveChanges() > 0)
                        {

                            book.File.SaveAs(path);
                            if (System.IO.File.Exists(oldImgPath))
                            {
                                System.IO.File.Delete(oldImgPath);
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "File must less than or equal to 1 MB";
                    }
                }
                else
                {
                    book.cover_image = Session["imgPath"].ToString();
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name", book.author_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", book.category_id);
            return View(book);
        }


        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            string currentImg = Request.MapPath(book.cover_image);
            db.Books.Remove(book);
            db.SaveChanges(); 
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
            }
            return RedirectToAction("Index");
        }

    }
}
