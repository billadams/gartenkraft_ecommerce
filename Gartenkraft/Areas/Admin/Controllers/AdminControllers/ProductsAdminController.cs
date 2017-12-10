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
    [Authorize(Roles = "Admin")]
    public class ProductsAdminController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: Admin/ProductsAdmin
        public ActionResult Index()
        {
            var tblProducts = db.tblProducts;
            foreach (var i in tblProducts) { i.SetPriceRange(); }
            ViewBag.Categories = db.tblProduct_Category.ToList();
            return View(tblProducts.ToList());
        }



        // GET: Admin/ProductsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            tblProduct.SetPriceRange();
            if (tblProduct == null)
            {
                return HttpNotFound();
            }

            return View(tblProduct);
        }

        // GET: Admin/ProductsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category.OrderBy(pc => pc.tblProduct_Line.product_line_id).OrderBy(pc => pc.category_name), "category_id", "category_name");
            return View();
        }

        // POST: Admin/ProductsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,product_short_description,product_long_description,product_category_id,product_date_added")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_category_id = new SelectList(db.tblProduct_Category.OrderBy(pc => pc.tblProduct_Line.product_line_id).OrderBy(pc => pc.category_name), "category_id", "category_name", tblProduct.product_category_id);
            return View(tblProduct);
        }

        // GET: Admin/ProductsAdmin/Edit/5
        public ActionResult Edit(int? id, string message, string errorMessage)
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
                        
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category.OrderBy(pc => pc.tblProduct_Line.product_line_id).OrderBy(pc => pc.category_name), "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == id).ToList();
            if (message != null)
            {
                ViewBag.UploadErrorMessage = message;
            }
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            tblProduct.SetPriceRange();
            return View(tblProduct);
        }

        // POST: Admin/ProductsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_short_description,product_long_description,product_category_id,product_date_added,soft_delete,is_visible,is_custom_product")] tblProduct tblProduct, int? optionID/*, string title*/, decimal? weight, decimal? unitCost, decimal? unitPrice)
        {
            string errorMessage = "";
            string editMessage = "";
            if (ModelState.IsValid)
            {
                var prodOptions = db.tblProduct_Option.Where(o => o.product_id == tblProduct.product_id).ToList();
                
                // saving product option for simple product
                if (tblProduct.is_custom_product == false && prodOptions.Count == 1 && optionID != null)
                {
                    var productSpec = prodOptions.FirstOrDefault();
                    if (productSpec == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    //productSpec.title = title;
                    productSpec.weight = Convert.ToDecimal(weight);
                    productSpec.unit_cost = Convert.ToDecimal(unitCost);
                    productSpec.unit_price = Convert.ToDecimal(unitPrice);
                    db.Entry(productSpec).State = EntityState.Modified;
                    var optionResult = db.SaveChanges();
                    if (optionResult <= 0)
                    {
                        errorMessage = "Unable to save product specifications. Please try again.";
                    }
                }

                if (errorMessage == "")
                {
                    var newProduct = db.tblProducts.Find(tblProduct.product_id);
                    newProduct.product_name = tblProduct.product_name;
                    newProduct.product_short_description = tblProduct.product_short_description;
                    newProduct.product_long_description = tblProduct.product_long_description;
                    newProduct.product_category_id = tblProduct.product_category_id;
                    newProduct.product_date_added = tblProduct.product_date_added;
                    newProduct.soft_delete = tblProduct.soft_delete;
                    newProduct.is_visible = tblProduct.is_visible;
                    // check if product type is valid
                    if (tblProduct.is_custom_product == false && prodOptions.Count > 1)
                    {
                        errorMessage = "This product is not qualified to be a simple product as it has multiple product options";
                        tblProduct.is_custom_product = true;
                    }
                    else
                    {
                        newProduct.is_custom_product = tblProduct.is_custom_product;
                    }
                    db.Entry(newProduct).State = EntityState.Modified;
                    var productResult = db.SaveChanges();
                    if (productResult <= 0)
                    {
                        editMessage = "Unable to edit product. Please try again.";
                    }
                }
            }
            else
            {
                editMessage = "Unable to save changes. Please review the information below.";
            }
            if (editMessage == "")
            {
                editMessage = "The product has been succesfully updated.";
            }
            tblProduct.SetPriceRange();
            ViewBag.EditMessage = editMessage;
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.product_category_id = new SelectList(db.tblProduct_Category.OrderBy(pc => pc.tblProduct_Line.product_line_id).OrderBy(pc => pc.category_name), "category_id", "category_name", tblProduct.product_category_id);
            ViewBag.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == tblProduct.product_id).ToList();
            return View(db.tblProducts.Find(tblProduct.product_id));
        }

        // GET: Admin/ProductsAdmin/Delete/5
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
            tblProduct.SetPriceRange();
            return View(tblProduct);
        }

        // POST: Admin/ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoicesContainsProduct = db.tblSales_Invoice_Lineitem.Where(li => li.product_id == id).ToList();
            tblProduct tblProduct = db.tblProducts.Find(id);

            // check if product had not been purchased before
            if (invoicesContainsProduct == null || invoicesContainsProduct.Count == 0)
            {
                //remove product
                db.tblProducts.Remove(tblProduct);
                db.SaveChanges();
            }
            else
            {
                tblProduct.soft_delete = true;
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

#region Product options

        // GET: Admin/ProductsAdmin/ProductOptions
        public PartialViewResult ProductOptions(int? id)
        {
            var pOptions = db.tblProduct_Option.Where(po => po.product_id == id).ToList();
            ViewBag.ProductID = id;
            return PartialView(pOptions);
        }

        // Get: Admin/ProductsAdmin/CreateOption
        public ActionResult CreateOption(int? id)
        {
            var product = db.tblProducts.Find(id);
            var prodOptions = db.tblProduct_Option.Where(o => o.product_id == id).ToList();
            if (product.is_custom_product == false && prodOptions.Count == 1)
            {
                string errorMessage = "This is a simple product. Please change to custom product before adding options.";
                return RedirectToAction("Edit", new { id = id, errorMessage = errorMessage });
            }
            var option = new tblProduct_Option() { product_id = Convert.ToInt32(id), tblProduct = db.tblProducts.Find(Convert.ToInt32(id)) };
            return PartialView(option);
        }

        // POST: Admin/ProductsAdmin/CreateOption
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOption([Bind(Include = "product_id,title, weight, unit_cost, unit_price")]tblProduct_Option option)
        {
            option.tblProduct = db.tblProducts.Find(option.product_id);

            if (ModelState.IsValid)
            {
                db.tblProduct_Option.Add(option);
                db.SaveChanges();
                return RedirectToAction("ProductOptions", new { id = option.product_id });
            }
            return PartialView(option);
        }

        // GET: Admin/ProductsAdmin/EditOption/5
        public ActionResult EditOption(int? id)
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
            return PartialView(tblProduct_Option);
        }

        // POST: Admin/ProductsAdmin/EditOption/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOption([Bind(Include = "title,weight,unit_cost,unit_price,product_id,option_id")] tblProduct_Option tblProduct_Option)
        {
            if (ModelState.IsValid)
            {
                var newOption = db.tblProduct_Option.Find(tblProduct_Option.option_id);
                newOption.title = tblProduct_Option.title;
                newOption.weight = tblProduct_Option.weight;
                newOption.unit_cost = tblProduct_Option.unit_cost;
                newOption.unit_price = tblProduct_Option.unit_price;

                db.Entry(newOption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductOptions", new { id = tblProduct_Option.product_id });
            }
            ViewBag.product_id = new SelectList(db.tblProducts, "product_id", "product_name", tblProduct_Option.product_id);
            return PartialView(tblProduct_Option);
        }

        // GET: Admin/ProductsAdmin/DeleteOption/5
        public ActionResult DeleteOption(int? id)
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
            return PartialView(tblProduct_Option);
        }

        // POST: Admin/ProductsAdmin/DeleteOption/5
        [HttpPost, ActionName("DeleteOption")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedOption(int id)
        {
            tblProduct_Option tblProduct_Option = db.tblProduct_Option.Find(id);
            db.tblProduct_Option.Remove(tblProduct_Option);
            db.SaveChanges();
            return RedirectToAction("ProductOptions", new { id = tblProduct_Option.product_id });
        }

#endregion

#region ProductImages

        public PartialViewResult ProductImages(int? id, string message)
        {
            var pImages = db.tblProduct_Image.Where(pi => pi.product_id == id).ToList();
            if (message != null)
            {
                if (message == "")
                {
                    message = "Product Images succesfully updated.";
                }
                ViewBag.Message = message;
            }            
            return PartialView(pImages);
        }

#endregion

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
