using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class Checkout
    {
        public Shipping ShippingData { get; set; }
        public SalesInvoice InvoiceData { get; set; }
        public BillingInfo BillingInformation { get; set; }
        public Cart CartInfo { get; set; }

    }
}