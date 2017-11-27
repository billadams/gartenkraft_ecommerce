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
        public ActionResult Show(int id)
        {
            var selectedProduct = db.vwProducts.FirstOrDefault(product => product.product_id == id);

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
