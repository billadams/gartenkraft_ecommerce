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
    public class ProductLineController : Controller
    {
        private GartenkraftCustomerEntities db = new GartenkraftCustomerEntities();

        // GET: ProductLine
        public ActionResult Index()
        {
            return View(db.vwProduct_Line.ToList());
        }

        // GET: ProductLine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwProduct_Line vwProduct_Line = db.vwProduct_Line.Find(id);
            if (vwProduct_Line == null)
            {
                return HttpNotFound();
            }
            return View(vwProduct_Line);
        }

        // GET: ProductLine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_line_id,product_line_name,is_visible")] vwProduct_Line vwProduct_Line)
        {
            if (ModelState.IsValid)
            {
                db.vwProduct_Line.Add(vwProduct_Line);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vwProduct_Line);
        }

        // GET: ProductLine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwProduct_Line vwProduct_Line = db.vwProduct_Line.Find(id);
            if (vwProduct_Line == null)
            {
                return HttpNotFound();
            }
            return View(vwProduct_Line);
        }

        // POST: ProductLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_line_id,product_line_name,is_visible")] vwProduct_Line vwProduct_Line)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vwProduct_Line).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vwProduct_Line);
        }

        // GET: ProductLine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwProduct_Line vwProduct_Line = db.vwProduct_Line.Find(id);
            if (vwProduct_Line == null)
            {
                return HttpNotFound();
            }
            return View(vwProduct_Line);
        }

        // POST: ProductLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vwProduct_Line vwProduct_Line = db.vwProduct_Line.Find(id);
            db.vwProduct_Line.Remove(vwProduct_Line);
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
