using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;
namespace Gartenkraft.Controllers
{
    public class CheckoutController : Controller
    {
        private Cart cart = new Cart();
        private GartenkraftEntities dbContext = new GartenkraftEntities();
        // GET: Checkout
        public ActionResult Index()
        {
            //initial checkout view
            cart = (Cart)Session["Cart"];
            return View();
        }
    }
}