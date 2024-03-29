﻿using System;
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
    public class ProductLinesAdminController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();


        // GET: Admin/ProductLinesAdmin
        public ActionResult Index()
        {
            return View(db.tblProduct_Line.ToList());
        }

        // GET: Admin/ProductLinesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Line tblProduct_Line = db.tblProduct_Line.Find(id);
            if (tblProduct_Line == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Line);
        }

        // GET: Admin/ProductLinesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductLinesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_line_id,product_line_name,date_added,soft_delete,is_visible")] tblProduct_Line tblProduct_Line)
        {
            if (ModelState.IsValid)
            {
                db.tblProduct_Line.Add(tblProduct_Line);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblProduct_Line);
        }

        // GET: Admin/ProductLinesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Line tblProduct_Line = db.tblProduct_Line.Find(id);
            if (tblProduct_Line == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Line);
        }

        // POST: Admin/ProductLinesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_line_id,product_line_name,date_added,soft_delete,is_visible")] tblProduct_Line tblProduct_Line)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct_Line).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblProduct_Line);
        }

        // GET: Admin/ProductLinesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Line tblProduct_Line = db.tblProduct_Line.Find(id);
            if (tblProduct_Line == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Line);
        }

        // POST: Admin/ProductLinesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct_Line tblProduct_Line = db.tblProduct_Line.Find(id);
            db.tblProduct_Line.Remove(tblProduct_Line);
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
