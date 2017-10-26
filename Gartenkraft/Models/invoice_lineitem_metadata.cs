using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(invoice_lineitem_metadata))]
    public partial class invoice_lineitem { }

    public class invoice_lineitem_metadata
    {
        [Display(Name = "Quantity")]
        public int lineitem_quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal LineTotal { get; set; }
    }
}