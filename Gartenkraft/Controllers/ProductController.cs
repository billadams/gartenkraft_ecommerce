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
            var products = db.vwProducts.Where(product => product.is_visible == true).ToList();
            foreach (var p in products) { p.SetPriceRange(); }
            return View(products);
        }

        // GET: vwProducts/ProductsByCategory
        public ActionResult ProductsByCategory(int categoryID)
        {
            var prodByCategory = db.vwProducts.Where(p => p.category_id == categoryID).ToList();
            foreach (var p in prodByCategory) { p.SetPriceRange(); }
            ViewBag.CategoryName = (string)db.vwCategories.Single(c => c.category_id == categoryID).category_name;
            return View(prodByCategory);
        }

        public ActionResult View(int productID)
        {
            var selectedProduct = db.vwProducts.Where(product => product.product_id == productID).SingleOrDefault();
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
