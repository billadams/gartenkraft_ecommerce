using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;
using Gartenkraft.ModelsForViews;

namespace Gartenkraft.Controllers
{
    public class NavController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();
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
            return PartialView(new MenuModel());
        }

        // GET: Nav
        public ActionResult Index()
        {
            return View();
        }
    }
}