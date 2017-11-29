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
    public class SalesInvoiceController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: Admin/Invoice
        public ActionResult Index()
        {
            var tblSales_Invoice = db.tblSales_Invoice;
            return View(tblSales_Invoice.ToList());
        }



        // GET: Admin/Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSales_Invoice tblSales_Invoice = db.tblSales_Invoice.Find(id);
            if (tblSales_Invoice == null)
            {
                return HttpNotFound();
            }
            return View(tblSales_Invoice);
        }

        // GET: Admin/Invoice/Create
        public ActionResult Create()
        {
            ViewBag.invoice_billing_id = new SelectList(db.tblBilling_Information.OrderBy(pc => pc.billing_id).OrderBy(pc => pc.billing_address1), "billing_id", "billing_address1");
            ViewBag.invoice_customer_id = new SelectList(db.AspNetUsers.OrderBy(pc => pc.Id).OrderBy(pc => pc.LastName), "Id", "LastName");
            ViewBag.invoice_shipping_id = new SelectList(db.tblShippings.OrderBy(pc => pc.shipping_id).OrderBy(pc => pc.shipping_address1), "shipping_id", "shipping_address1");
            return View();
        }

        // POST: Admin/Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoice_date,customer_id,billing_id,shipping_id")] tblSales_Invoice tblSales_Invoice)
        {
            if (ModelState.IsValid)
            {
                db.tblSales_Invoice.Add(tblSales_Invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invoice_billing_id = new SelectList(db.tblBilling_Information.OrderBy(pc => pc.billing_id).OrderBy(pc => pc.billing_address1), "billing_id", "billing_address1",tblSales_Invoice.billing_id);
            ViewBag.invoice_customer_id = new SelectList(db.AspNetUsers.OrderBy(pc => pc.Id).OrderBy(pc => pc.LastName), "Id", "LastName",tblSales_Invoice.customer_id);
            ViewBag.invoice_shipping_id = new SelectList(db.tblShippings.OrderBy(pc => pc.shipping_id).OrderBy(pc => pc.shipping_address1), "shipping_id", "shipping_address1",tblSales_Invoice.shipping_id);
            return View(tblSales_Invoice);
        }

        // GET: Admin/Invoice/Edit/5
        public ActionResult Edit(int? id, string message, string errorMessage)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSales_Invoice tblSales_Invoice = db.tblSales_Invoice.Find(id);
            if (tblSales_Invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.invoice_billing_id = new SelectList(db.tblBilling_Information.OrderBy(pc => pc.billing_id).OrderBy(pc => pc.billing_address1), "billing_id", "billing_address1", tblSales_Invoice.billing_id);
            ViewBag.invoice_customer_id = new SelectList(db.AspNetUsers.OrderBy(pc => pc.Id).OrderBy(pc => pc.LastName), "Id", "LastName", tblSales_Invoice.customer_id);
            ViewBag.invoice_shipping_id = new SelectList(db.tblShippings.OrderBy(pc => pc.shipping_id).OrderBy(pc => pc.shipping_address1), "shipping_id", "shipping_address1", tblSales_Invoice.shipping_id);
            if (message != null)
            {
                ViewBag.UploadErrorMessage = message;
            }
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            return View(tblSales_Invoice);
        }

        // POST: Admin/Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoice_id,invoice_date,customer_id,billing_id,shipping_id")] tblSales_Invoice tblSales_Invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSales_Invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invoice_billing_id = new SelectList(db.tblBilling_Information.OrderBy(pc => pc.billing_id).OrderBy(pc => pc.billing_address1), "billing_id", "billing_address1", tblSales_Invoice.billing_id);
            ViewBag.invoice_customer_id = new SelectList(db.AspNetUsers.OrderBy(pc => pc.Id).OrderBy(pc => pc.LastName), "Id", "LastName", tblSales_Invoice.customer_id);
            ViewBag.invoice_shipping_id = new SelectList(db.tblShippings.OrderBy(pc => pc.shipping_id).OrderBy(pc => pc.shipping_address1), "shipping_id", "shipping_address1", tblSales_Invoice.shipping_id);
            return View(tblSales_Invoice);
        }

        // GET: Admin/SalesInvoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSales_Invoice tblSales_Invoice = db.tblSales_Invoice.Find(id);
            if (tblSales_Invoice == null)
            {
                return HttpNotFound();
            }
            return View(tblSales_Invoice);
        }

        // POST: Admin/SalesInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSales_Invoice tblSales_Invoice = db.tblSales_Invoice.Find(id);
            db.tblSales_Invoice.Remove(tblSales_Invoice);
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