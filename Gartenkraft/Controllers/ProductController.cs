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
    public class ProductController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: vwProducts
        public ActionResult Index()
        {
            return View(db.vwProducts.Where(product => product.is_visible == true).ToList());
        }

        // GET: vwProducts/ProductsByCategory
        public ActionResult ProductsByCategory(int categoryID)
        {
            var prodByCategory = db.vwProducts.Where(p => p.product_category_id == categoryID).ToList();
            foreach (var p in prodByCategory)
            {
                p.ProductImages = db.tblProduct_Image.Where(img => img.product_id == p.product_id).ToList();
                p.SetPriceRange();
            }
            ViewBag.CategoryName = (string)db.vwCategories.Where(c => c.category_id == categoryID).Single().category_name;
            return View(prodByCategory);
        }

        public ActionResult View(int productID)
        {
            var selectedProduct = db.vwProducts.Single(product => product.product_id == productID);
            selectedProduct.ProductImages = db.tblProduct_Image.Where(img => img.product_id == p.product_id).ToList();
            selectedProduct.SetPriceRange();
            return View(selectedProduct);
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
