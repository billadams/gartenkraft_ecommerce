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
    public class ProductsAdminController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: Products
        public ActionResult Index()
        {
            var tblProducts = db.tblProducts.Include(t => t.tblProduct_Line).Include(t => t.tblProduct_Category);
            ViewBag.Categories = db.tblProduct_Category.ToList();
            return View(tblProducts.ToList());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name");
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,product_short_description,product_long_description,product_unit_cost,product_unit_price,product_category_id,product_line_id,product_weight,product_date_added,soft_delete,is_visible")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name", tblProduct.product_line_id);
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name", tblProduct.product_category_id);
            return View(tblProduct);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id, string message)
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
            ViewBag.product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name", tblProduct.product_line_id);
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == id).ToList();
            if (message != null)
            {
                ViewBag.Message = message;
            }
            return View(tblProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_short_description,product_long_description,product_unit_cost,product_unit_price,product_category_id,product_line_id,product_weight,product_date_added,soft_delete,is_visible")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name", tblProduct.product_line_id);
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category, "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == tblProduct.product_id).ToList();
            return View(tblProduct);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
