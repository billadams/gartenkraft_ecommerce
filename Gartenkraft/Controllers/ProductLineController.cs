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
    public class ProductLineController : Controller
    {
        private GartenkraftCustomerEntities db = new GartenkraftCustomerEntities();

        // GET: ProductLine
        public ActionResult Index()
        {
            return View(db.vwProduct_Line.ToList());
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
