using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(InvoiceTableMetadata))]
    public partial class tblSales_Invoice { }

    public class InvoiceTableMetadata
    {
        [Display(Name = "Invoice Number")]
        public int invoice_id { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime invoice_date { get; set; }

        public string customer_id { get; set; }

        [Display(Name = "Billing Number")]
        public Nullable<int> billing_id { get; set; }

        [Display(Name = "Shipping Number")]
        public Nullable<int> shipping_id { get; set; }

        [Required(ErrorMessage = "Please enter your Email here.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string invoice_email { get; set; }

        [Required]
        [Display(Name = "Is Guest Checkout")]
        public bool is_guest { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string customer_first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string customer_last_name { get; set; }
    }
}