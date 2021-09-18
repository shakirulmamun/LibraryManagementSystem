using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.TblUser.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUser.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Username,UserPass,UserType")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                var userId = db.TblUser.Max(o => o.UserID) + 1;
                var oTblUser = new TblUser();
                oTblUser.UserID = userId;
                oTblUser.Username = tblUser.Username;
                oTblUser.UserPass = tblUser.UserPass;
                oTblUser.UserType = tblUser.UserType;
                db.TblUser.Add(oTblUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblUser);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUser.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Username,UserPass,UserType")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblUser);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUser tblUser = await db.TblUser.FindAsync(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TblUser tblUser = await db.TblUser.FindAsync(id);
            db.TblUser.Remove(tblUser);
            await db.SaveChangesAsync();
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

        public async Task<ActionResult> UserRole(int userId)
        {
            var listTblUserRole = await db.TblUserRole.Where(o => o.UserID == userId).ToListAsync();
            var oTblUser = await db.TblUser.Where(o => o.UserID == userId).FirstOrDefaultAsync();
            TempData["Username"] = oTblUser.Username;

            #region create menu at view page
            var listUserRole = new List<TblUserRole>();

            #region Books
            var oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Books").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Books"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Purchases
             oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Purchases").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Purchases"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region Category
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Categories").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new TblUserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Categories";
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion
            #endregion

            return View(listUserRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRole(TblUserRole[] tblUserRoles)
        {
            var userRoleIdMax = db.TblUserRole.Max(o => (int?)o.UserRoleID) ?? 0;
            for (var i = 0; i < tblUserRoles.Length; i++)
            {
                int userRoleId = tblUserRoles[i].UserRoleID;
                var oTblUserRole = await db.TblUserRole.Where(o => o.UserRoleID == userRoleId).FirstOrDefaultAsync();
                if (oTblUserRole == null) // insert
                {
                    oTblUserRole = new TblUserRole();
                    oTblUserRole.UserRoleID = ++userRoleIdMax;
                    oTblUserRole.UserID = tblUserRoles[i].UserID;
                    oTblUserRole.PageName = tblUserRoles[i].PageName;
                    oTblUserRole.IsCreate = tblUserRoles[i].IsCreate;
                    oTblUserRole.IsRead = tblUserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = tblUserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = tblUserRoles[i].IsDelete;
                    db.TblUserRole.Add(oTblUserRole);
                }
                else // update
                {
                    oTblUserRole.IsCreate = tblUserRoles[i].IsCreate;
                    oTblUserRole.IsRead = tblUserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = tblUserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = tblUserRoles[i].IsDelete;
                }
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
