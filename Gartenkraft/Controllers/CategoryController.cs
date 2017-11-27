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
    public class CategoryController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.vwCategories.ToList());
        }

        // GET: vwProducts/ProductsByCategory
        public ActionResult Show(int id)
        {
            var prodByCategory = db.vwProducts.Where(products => products.category_id == id && products.is_visible == true).ToList();

            foreach (var product in prodByCategory)
            {
                product.SetPriceRange();
            }

            ViewBag.CategoryName = db.vwCategories.FirstOrDefault(category => category.category_id == id).category_name;

            return View(prodByCategory);
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
