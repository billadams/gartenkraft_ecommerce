using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Areas.Admin.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }
        // GET: Admin/User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            if (AspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUsers);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName, Address1,Address2,City,State,Zip,Zip4,Country,ImageFileName")] AspNetUser AspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(AspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(AspNetUsers);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            if (AspNetUsers == null)
            {
                return HttpNotFound();
            }

            return View(AspNetUsers);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName, Address1,Address2,City,State,Zip,Zip4,Country,ImageFileName")] AspNetUser AspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(AspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AspNetUsers);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            if (AspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUsers);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(AspNetUsers);
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
