using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult SubmitOrder(Checkout oCheckout, string sameBilling)
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Cart");//no cart found, shouldn't be able to view this page
            }
            //grab cart info from session don't rely on passed in checkout
            Cart validCartInfo = (Cart) Session["Cart"];
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Select(x => x.Value.Errors)
            //        .Where(y => y.Count > 0)
            //        .ToList();
            //    return RedirectToAction("Index", "Checkout", );
            //    //var message = string.Join(" | ", ModelState.Values
            //    //    .SelectMany(v => v.Errors)
            //    //    .Select(e => e.ErrorMessage));
            //    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            //}
            oCheckout = CheckforNullableValues(oCheckout);
            CheckoutDB oCheckoutDb = new CheckoutDB();
            if (sameBilling == "true")
            {
                oCheckout = BillingSameasShippingInfo(oCheckout);
            }

            //if (Session["User"] == null)
            //{
            //guest logic

            oCheckout.InvoiceData.ShippingID = oCheckoutDb.SaveGuestShippingOrder(oCheckout.ShippingData);
            oCheckout.InvoiceData.BillingID = oCheckoutDb.SaveGuestBillingOrder(oCheckout.BillingInformation);
            oCheckout.InvoiceData.CustomerFirstName = oCheckout.BillingInformation.BillingFirstName;
            oCheckout.InvoiceData.CustomerLastName = oCheckout.BillingInformation.BillingLastName;
            oCheckout.InvoiceData.InvoiceDate = DateTime.Now;
            oCheckout.InvoiceData.InvoiceID = oCheckoutDb.SaveGuestInvoice(oCheckout.InvoiceData);


            //invoice table logic
            foreach(InvoiceLineItemTable cartItem in validCartInfo.CartItems)
            {
                oCheckoutDb.SaveLineItem(cartItem.product_id, cartItem.lineitem_quantity,
                    oCheckout.InvoiceData.InvoiceID, cartItem.product_option_id);
            }

            
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

        public Checkout BillingSameasShippingInfo(Checkout oCheckout)
        {
            oCheckout.BillingInformation.BillingAddress = oCheckout.ShippingData.ShippingAddress;
            oCheckout.BillingInformation.BillingAddress2 = oCheckout.ShippingData.ShippingAddress2;
            oCheckout.BillingInformation.BillingFirstName = oCheckout.ShippingData.ShippingFirstName;
            oCheckout.BillingInformation.BillingLastName = oCheckout.ShippingData.ShippingLastName;
            oCheckout.BillingInformation.BillingCity = oCheckout.ShippingData.ShippingCity;
            oCheckout.BillingInformation.BillingState = oCheckout.ShippingData.ShippingState;
            oCheckout.BillingInformation.BillingCountry = oCheckout.ShippingData.ShippingCountry;
            oCheckout.BillingInformation.BillingZip = oCheckout.ShippingData.ShippingZip;
            oCheckout.BillingInformation.BillingZip4 = oCheckout.ShippingData.ShippingZip4;            
            return oCheckout;
        }
    }
}