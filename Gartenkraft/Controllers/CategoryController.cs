using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Controllers
{
    public class CategoryController : Controller
    {
        private GartenkraftCustomerEntities db = new GartenkraftCustomerEntities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.vwCategories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwCategory vwCategory = db.vwCategories.Find(id);
            if (vwCategory == null)
            {
                return HttpNotFound();
            }
            return View(vwCategory);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_name,category_id,product_category_image_name,category_product_line_id,category_image_id,soft_delete,is_visible")] vwCategory vwCategory)
        {
            if (ModelState.IsValid)
            {
                db.vwCategories.Add(vwCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vwCategory);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwCategory vwCategory = db.vwCategories.Find(id);
            if (vwCategory == null)
            {
                return HttpNotFound();
            }
            return View(vwCategory);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_name,category_id,product_category_image_name,category_product_line_id,category_image_id,soft_delete,is_visible")] vwCategory vwCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vwCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vwCategory);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwCategory vwCategory = db.vwCategories.Find(id);
            if (vwCategory == null)
            {
                return HttpNotFound();
            }
            return View(vwCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            vwCategory vwCategory = db.vwCategories.Find(id);
            db.vwCategories.Remove(vwCategory);
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
