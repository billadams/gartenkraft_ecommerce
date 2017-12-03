using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(InvoiceViewMetadata))]
    public partial class vwInvoice { }

    public class InvoiceViewMetadata
    {
        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime invoice_date { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "")]
        public string ImageFileName { get; set; }

        [Display(Name = "Billing Address")]
        public string billing_address1 { get; set; }

        [Display(Name = "")]
        public string billing_address2 { get; set; }

        [Display(Name = "Billing City")]
        public string billing_city { get; set; }

        [Display(Name = "Billing State")]
        public string billing_state { get; set; }

        [Display(Name = "Billing Zip")]
        public string billing_zip { get; set; }

        [Display(Name = "")]
        public string billing_zip4 { get; set; }

        [Display(Name = "Billing Country")]
        public string billing_country { get; set; }

        [Display(Name = "Shipping Address")]
        public string shipping_address1 { get; set; }

        [Display(Name = "")]
        public string shipping_address2 { get; set; }

        [Display(Name = "Shipping City")]
        public string shipping_city { get; set; }

        [Display(Name = "Shipping State")]
        public string shipping_state { get; set; }

        [Display(Name = "Shipping Zip")]
        public string shipping_zip { get; set; }

        [Display(Name = "")]
        public string shipping_zip4 { get; set; }

        [Display(Name = "Shipping Country")]
        public string shipping_country { get; set; }
    }
}