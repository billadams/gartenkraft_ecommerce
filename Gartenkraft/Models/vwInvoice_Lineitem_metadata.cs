using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(vwInvoice_Lineitem_metadata))]
    public partial class vwInvoice_Lineitem { }

    public class vwInvoice_Lineitem_metadata
    {
        [Display(Name = "Quantity")]
        public int lineitem_quantity { get; set; }

        [Display(Name = "")]
        public int invoice_id { get; set; }

        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Display(Name = "Short Description")]
        public string product_short_description { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal product_unit_price { get; set; }

        [Display(Name = "Category")]
        public int product_category_id { get; set; }

        [Display(Name = "Line")]
        public int product_line_id { get; set; }

        [Display(Name = "Weight")]
        public decimal product_weight { get; set; }
    }
}