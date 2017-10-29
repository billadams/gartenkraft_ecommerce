using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Controllers
{
    public class NavController : Controller
    {
        private GartenkraftCustomerEntities db = new GartenkraftCustomerEntities();
        private vwProduct_Line _productLines;
        private vwCategory _categories;

        public NavController() { }

        public NavController(vwProduct_Line productLines, vwCategory categories)
        {
            this._productLines = productLines;
            this._categories = categories;
        }

        public PartialViewResult Menu()
        {
            List<vwProduct_Line> _productLines = db.vwProduct_Line.ToList();

            return PartialView(_productLines);
        }

        // GET: Nav
        public ActionResult Index()
        {
            return View();
        }
    }
}