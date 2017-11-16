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
        private Checkout oCheckout = new Checkout();
        private GartenkraftEntities dbContext = new GartenkraftEntities();
        // GET: Checkout
        public ActionResult Index()
        {
            if (Session["Cart"] != null)
            {
                oCheckout.CartInfo = (Cart) Session["Cart"];

                return View(oCheckout);
            }
            else
            {
                return RedirectToAction("Index", "Cart");//no cart found, shouldn't be able to view this page
            }
            
        }

        public ActionResult SubmitOrder()
        {

            return View();
        }
    }
}