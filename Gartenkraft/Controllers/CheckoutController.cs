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
                var test = oCheckout.CartInfo.Total;
                var test2 = oCheckout.CartInfo.Subtotal;
                return View(oCheckout);
            }
            else
            {
                return RedirectToAction("Index", "Cart");//no cart found, shouldn't be able to view this page
            }
            
        }

        public ActionResult SubmitOrder(Checkout oCheckout)
        {
            //grab cart info from session don't rely on passed in checkout
            Cart validCartInfo = (Cart) Session["Cart"];
            oCheckout = CheckforNullableValues(oCheckout);
            //hardcoding customerid to 0 until customer is setup
            oCheckout.BillingInformation.CustomerID = 0;
            oCheckout.ShippingData.CustomerID = 0;
            oCheckout.InvoiceData = new SalesInvoice();
            oCheckout.InvoiceData.CustomerID = 0;
            CheckoutDB oCheckoutDb = new CheckoutDB();
            oCheckout.InvoiceData.ShippingID= oCheckoutDb.SaveShippingOrder(oCheckout.ShippingData);
            oCheckout.InvoiceData.BillingID = oCheckoutDb.SaveBillingOrder(oCheckout.BillingInformation);
            oCheckout.InvoiceData.InvoiceDate = DateTime.Now;
            oCheckout.InvoiceData.InvoiceID = oCheckoutDb.SaveInvoice(oCheckout.InvoiceData);
            //clearing session or you could just uncomment below line if you want to remove car
            //Session["Cart"] = null;
            Session.Clear();
            Session.Abandon();

            return View(oCheckout);
        }

        public Checkout CheckforNullableValues(Checkout oCheckout)
        {
            if (String.IsNullOrEmpty(oCheckout.ShippingData.ShippingAddress2))
            {
                oCheckout.ShippingData.ShippingAddress2 = "";
            }
            if (String.IsNullOrEmpty(oCheckout.ShippingData.ShippingZip4))
            {
                oCheckout.ShippingData.ShippingZip4 = "";
            }
            if (String.IsNullOrEmpty(oCheckout.BillingInformation.BillingAddress2))
            {
                oCheckout.BillingInformation.BillingAddress2 = "";
            }
            if (String.IsNullOrEmpty(oCheckout.BillingInformation.BillingZip4))
            {
                oCheckout.BillingInformation.BillingZip4 = "";
            }
            return oCheckout;
        }
    }
}