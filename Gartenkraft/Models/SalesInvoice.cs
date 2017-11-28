using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class SalesInvoice
    {
        public int InvoiceID { get; set; }
        public int CustomerID { get; set; }
        public int BillingID {  get; set; }
        public int ShippingID { get; set; }
        public DateTime InvoiceDate { get; set; }

    }
}