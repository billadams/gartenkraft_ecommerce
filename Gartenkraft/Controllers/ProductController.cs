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

            foreach (var product in products)
            {
                product.SetPriceRange();
            }

            return View(products);
        }

        // GET: vwProducts/ProductsByCategory
        public ActionResult ProductsByCategory(int categoryID)
        {
            var prodByCategory = db.vwProducts.Where(products => products.category_id == categoryID && products.is_visible == true).ToList();

            foreach (var product in prodByCategory)
            {
                product.SetPriceRange();
            }

            ViewBag.CategoryName = db.vwCategories.FirstOrDefault(category => category.category_id == categoryID).category_name;

            return View(prodByCategory);
        }

        public ActionResult View(int productID)
        {
            var selectedProduct = db.vwProducts.FirstOrDefault(product => product.product_id == productID);

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
