using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Controllers.AdminControllers
{
    public class tblProduct_CategoryController : Controller
    {
        private gartenkraftModelEntities db = new gartenkraftModelEntities();

        // GET: tblProduct_Category
        public ActionResult Index()
        {
            var tblProduct_Category = db.tblProduct_Category.Include(t => t.tblStore);
            return View(tblProduct_Category.ToList());
        }

        // GET: tblProduct_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            if (tblProduct_Category == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Category);
        }

        // GET: tblProduct_Category/Create
        public ActionResult Create()
        {
            ViewBag.category_store_id = new SelectList(db.tblStores, "store_id", "store_name");
            return View();
        }

        // POST: tblProduct_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,category_store_id,category_image_id,category_name")] tblProduct_Category tblProduct_Category)
        {
            if (ModelState.IsValid)
            {
                db.tblProduct_Category.Add(tblProduct_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_store_id = new SelectList(db.tblStores, "store_id", "store_name", tblProduct_Category.category_store_id);
            return View(tblProduct_Category);
        }

        // GET: tblProduct_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            if (tblProduct_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_store_id = new SelectList(db.tblStores, "store_id", "store_name", tblProduct_Category.category_store_id);
            return View(tblProduct_Category);
        }

        // POST: tblProduct_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,category_store_id,category_image_id,category_name")] tblProduct_Category tblProduct_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_store_id = new SelectList(db.tblStores, "store_id", "store_name", tblProduct_Category.category_store_id);
            return View(tblProduct_Category);
        }

        // GET: tblProduct_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            if (tblProduct_Category == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Category);
        }

        // POST: tblProduct_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            db.tblProduct_Category.Remove(tblProduct_Category);
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
