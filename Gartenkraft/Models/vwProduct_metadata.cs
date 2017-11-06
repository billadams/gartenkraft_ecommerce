using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(vwProductMetadata))]
    public partial class vwProduct
    {
        [NotMapped]
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }

    public class vwProductMetadata
    {
        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Display(Name = "Short Description")]
        public string product_short_description { get; set; }

        [Display(Name = "Long Description")]
        public string product_long_description { get; set; }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal product_unit_price { get; set; }

        [Display(Name = "Weight")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal product_weight { get; set; }
    }
}