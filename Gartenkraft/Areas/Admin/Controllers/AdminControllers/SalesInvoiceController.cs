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
        public ActionResult Create([Bind(Include = "invoice_id,invoice_date,customer_id,billing_id,shipping_id")] tblSales_Invoice tblSales_Invoice)
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

        //// POST: Admin/Invoice/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "invoice_id,invoice_date,customer_id,billing_id,shipping_id")] tblSales_Invoice tblSales_Invoice)
        //{
        //    string errorMessage = "";
        //    if (ModelState.IsValid)
        //    {
        //        var prodOptions = db.tblProduct_Option.Where(o => o.product_id == tblSales_Invoice.invoice_id).ToList();

        //        if (errorMessage == "")
        //        {
        //            db.Entry(tblSales_Invoice).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    ViewBag.ErrorMessage = errorMessage;
        //    var tblSales_Invoice = db.tblSales_Invoice;
        //    foreach (var i in tblProducts) { i.SetPriceRange(); }

        //    ViewBag.product_category_id = new SelectList(db.tblProduct_Category.OrderBy(pc => pc.tblProduct_Line.product_line_id).OrderBy(pc => pc.category_name), "category_id", "category_name", tblProduct.product_category_id);
        //    ViewBag.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == tblProduct.product_id).ToList();
        //    return View(tblProduct);
        //}

        //// GET: Admin/ProductsAdmin/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblProduct tblProduct = db.tblProducts.Find(id);
        //    if (tblProduct == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    tblProduct.SetPriceRange();
        //    return View(tblProduct);
        //}

        //// POST: Admin/ProductsAdmin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var invoicesContainsProduct = db.tblSales_Invoice_Lineitem.Where(li => li.product_id == id).ToList();
        //    tblProduct tblProduct = db.tblProducts.Find(id);

        //    // check if product had not been purchased before
        //    if (invoicesContainsProduct == null || invoicesContainsProduct.Count == 0)
        //    {
        //        //remove product
        //        db.tblProducts.Remove(tblProduct);
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        tblProduct.soft_delete = true;
        //        db.Entry(tblProduct).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

        //#region Product options

        //// Get: Admin/ProductsAdmin/CreateOption
        //public ActionResult CreateOption(int? id)
        //{
        //    var product = db.tblProducts.Find(id);
        //    var prodOptions = db.tblProduct_Option.Where(o => o.product_id == id).ToList();
        //    if (product.is_custom_product == false && prodOptions.Count == 1)
        //    {
        //        string errorMessage = "This is a simple product. Please change to custom product before adding options.";
        //        return RedirectToAction("Edit", new { id = id, errorMessage = errorMessage });
        //    }
        //    var option = new tblProduct_Option() { product_id = Convert.ToInt32(id), tblProduct = db.tblProducts.Find(Convert.ToInt32(id)) };
        //    return View(option);
        //}

        //// POST: Admin/ProductsAdmin/CreateOption
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateOption([Bind(Include = "product_id,title, weight, unit_cost, unit_price")]tblProduct_Option option)
        //{
        //    option.tblProduct = db.tblProducts.Find(option.product_id);

        //    if (ModelState.IsValid)
        //    {
        //        db.tblProduct_Option.Add(option);
        //        db.SaveChanges();
        //        return RedirectToAction("Edit", new { id = option.product_id });
        //    }
        //    return View(option);
        //}

        //// GET: Admin/ProductsAdmin/EditOption/5
        //public ActionResult EditOption(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
        //    if (tblProduct_Option == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name", tblProduct_Option.product_id);
        //    return View(tblProduct_Option);
        //}

        //// POST: Admin/ProductsAdmin/EditOption/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditOption([Bind(Include = "title,weight,unit_cost,unit_price,product_id,option_id")] tblProduct_Option tblProduct_Option)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tblProduct_Option).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Edit", new { id = tblProduct_Option.product_id });
        //    }
        //    ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name", tblProduct_Option.product_id);
        //    return View(tblProduct_Option);
        //}

        //// GET: Admin/ProductsAdmin/DeleteOption/5
        //public ActionResult DeleteOption(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
        //    if (tblProduct_Option == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblProduct_Option);
        //}

        //// POST: Admin/ProductsAdmin/DeleteOption/5
        //[HttpPost, ActionName("DeleteOption")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmedOption(int id)
        //{
        //    tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
        //    db.tblProduct_Option.Remove(tblProduct_Option);
        //    db.SaveChanges();
        //    return RedirectToAction("Edit", new { id = tblProduct_Option.product_id });
        //}

        //#endregion

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}