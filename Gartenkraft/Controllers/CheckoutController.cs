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

        public ActionResult SubmitOrder(Checkout oCheckout)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Cart");//no cart found, shouldn't be able to view this page
            }
            //grab cart info from session don't rely on passed in checkout
            Cart validCartInfo = (Cart) Session["Cart"];
            oCheckout = CheckforNullableValues(oCheckout);
            CheckoutDB oCheckoutDb = new CheckoutDB();
            //if (Session["User"] == null)
            //{
            //guest logic
            //testing for line item insertion
            var cartItems = validCartInfo.CartItems;

            oCheckout.InvoiceData.ShippingID = oCheckoutDb.SaveGuestShippingOrder(oCheckout.ShippingData);
            oCheckout.InvoiceData.BillingID = oCheckoutDb.SaveGuestBillingOrder(oCheckout.BillingInformation);
            oCheckout.InvoiceData.CustomerFirstName = oCheckout.BillingInformation.BillingFirstName;
            oCheckout.InvoiceData.CustomerLastName = oCheckout.BillingInformation.BillingLastName;
            oCheckout.InvoiceData.InvoiceDate = DateTime.Now;
            oCheckout.InvoiceData.InvoiceID = oCheckoutDb.SaveGuestInvoice(oCheckout.InvoiceData);

            //will move to here for line item insertion after
            
            //}
            //else
            //{
            //    //do customerid logic
            //}

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