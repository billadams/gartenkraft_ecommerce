using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(SalesInvoice))]

    public class SalesInvoice
    {
        public int InvoiceID { get; set; }
        public int CustomerID { get; set; }
        public int BillingID {  get; set; }
        public int ShippingID { get; set; }
        public DateTime InvoiceDate { get; set; }

    }
}