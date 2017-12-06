using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(tblSales_Invoice_LineitemMetadata))]
    public partial class tblSales_Invoice_Lineitem { }

    public class tblSales_Invoice_LineitemMetadata
    {
        [Display(Name = "Quantity")]
        public int lineitem_quantity { get; set; }
    }
}