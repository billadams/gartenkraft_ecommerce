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
        private GartenkraftCustomerEntities db = new GartenkraftCustomerEntities();

        // GET: vwProducts
        public ActionResult Index()
        {
            //return View(db.vwProducts.Where(vP => vP.is_visible == true).ToList());
            return View(db.vwProducts.Where(product => product.is_visible == true).ToList());
            //return View();
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
