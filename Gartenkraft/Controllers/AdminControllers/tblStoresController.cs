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
    public class tblStoresController : Controller
    {
        private gartenkraftModelEntities db = new gartenkraftModelEntities();

        // GET: tblStores
        public ActionResult Index()
        {
            return View(db.tblStores.ToList());
        }

        // GET: tblStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStore tblStore = db.tblStores.Find(id);
            if (tblStore == null)
            {
                return HttpNotFound();
            }
            return View(tblStore);
        }

        // GET: tblStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_id,store_name,date_added,soft_delete")] tblStore tblStore)
        {
            if (ModelState.IsValid)
            {
                db.tblStores.Add(tblStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblStore);
        }

        // GET: tblStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStore tblStore = db.tblStores.Find(id);
            if (tblStore == null)
            {
                return HttpNotFound();
            }
            return View(tblStore);
        }

        // POST: tblStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_id,store_name,date_added,soft_delete")] tblStore tblStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblStore);
        }

        // GET: tblStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStore tblStore = db.tblStores.Find(id);
            if (tblStore == null)
            {
                return HttpNotFound();
            }
            return View(tblStore);
        }

        // POST: tblStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStore tblStore = db.tblStores.Find(id);
            db.tblStores.Remove(tblStore);
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
