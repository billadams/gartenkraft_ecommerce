using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    public class SalesInvoiceTableMetadata
    {
        public int InvoiceID { get; set; }
        public string CustomerID { get; set; }
        public int BillingID {  get; set; }
        public int ShippingID { get; set; }
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [DisplayName("Email")]
        [EmailAddress]
        public string InvoiceEmail { get; set; }
        public bool IsGuest { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}