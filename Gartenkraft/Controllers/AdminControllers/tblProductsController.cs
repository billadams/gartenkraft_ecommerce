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
    public class tblProductsController : Controller
    {
        private gartenkraftModelEntities db = new gartenkraftModelEntities();

        // GET: tblProducts
        public ActionResult Index()
        {
            var tblProducts = db.tblProducts.Include(t => t.tblProduct_Category).Include(t => t.tblStore);
            return View(tblProducts.ToList());
        }

        // GET: tblProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: tblProducts/Create
        public ActionResult Create()
        {
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name");
            ViewBag.product_store_id = new SelectList(db.tblStores, "store_id", "store_name");
            return View();
        }

        // POST: tblProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,product_short_description,product_long_description,product_unit_cost,product_unit_price,product_category_id,product_store_id,product_weight,product_date_added,product_image_id")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.product_store_id = new SelectList(db.tblStores, "store_id", "store_name", tblProduct.product_store_id);
            return View(tblProduct);
        }

        // GET: tblProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.product_store_id = new SelectList(db.tblStores, "store_id", "store_name", tblProduct.product_store_id);
            return View(tblProduct);
        }

        // POST: tblProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_short_description,product_long_description,product_unit_cost,product_unit_price,product_category_id,product_store_id,product_weight,product_date_added,product_image_id")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.product_store_id = new SelectList(db.tblStores, "store_id", "store_name", tblProduct.product_store_id);
            return View(tblProduct);
        }

        // GET: tblProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // POST: tblProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblProduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblProduct);
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
