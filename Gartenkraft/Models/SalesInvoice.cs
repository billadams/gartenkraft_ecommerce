using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class SalesInvoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

    }
}