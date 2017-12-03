using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(InvoiceLineitemTableMetadata))]
    public partial class InvoiceLineItemTable { }

    public class InvoiceLineitemTableMetadata
    {
        [Display(Name = "Quantity")]
        public int lineitem_quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal LineTotal { get; set; }
    }
}