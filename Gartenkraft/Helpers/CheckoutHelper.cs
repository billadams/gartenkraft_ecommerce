using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gartenkraft.Models;

namespace Gartenkraft.Helpers
{
    public class CheckoutHelper
    {
        public static Checkout CheckforNullableValues(Checkout oCheckout)
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

        public static Checkout BillingSameasShippingInfo(Checkout oCheckout)
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