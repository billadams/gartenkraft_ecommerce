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
        private List<vwProduct_Line> _productLines;
        //private vwCategory _categories;

        //public NavController(vwProduct_Line productLines, vwCategory categories)
        //{
        //    this.productLines = productLines;
        //    this.categories = categories;
        //}

        public PartialViewResult Menu()
        {
            _productLines = db.vwProduct_Line.ToList();

            return PartialView(_productLines);
        }

        // GET: Nav
        public ActionResult Index()
        {
            return View();
        }
    }
}