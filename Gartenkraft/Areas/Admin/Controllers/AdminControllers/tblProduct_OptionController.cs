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
    public class tblProduct_OptionController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: Admin/tblProduct_Option
        public ActionResult Index()
        {
            var tblProduct_Option = db.tblProduct_Option.Include(t => t.tblProduct);
            return View(tblProduct_Option.ToList());
        }

        // GET: Admin/tblProduct_Option/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
            if (tblProduct_Option == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Option);
        }

        // GET: Admin/tblProduct_Option/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name");
            return View();
        }

        // POST: Admin/tblProduct_Option/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,weight,unit_cost,unit_price,product_id,product_option_id")] tblProduct_Option tblProduct_Option)
        {
            if (ModelState.IsValid)
            {
                db.tblProduct_Option.Add(tblProduct_Option);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name", tblProduct_Option.product_id);
            return View(tblProduct_Option);
        }

        // GET: Admin/tblProduct_Option/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
            if (tblProduct_Option == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name", tblProduct_Option.product_id);
            return View(tblProduct_Option);
        }

        // POST: Admin/tblProduct_Option/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title,weight,unit_cost,unit_price,product_id,product_option_id")] tblProduct_Option tblProduct_Option)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct_Option).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name", tblProduct_Option.product_id);
            return View(tblProduct_Option);
        }

        // GET: Admin/tblProduct_Option/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
            if (tblProduct_Option == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Option);
        }

        // POST: Admin/tblProduct_Option/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
            db.tblProduct_Option.Remove(tblProduct_Option);
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
