//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gartenkraft.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSales_Invoice_Lineitem
    {
        public int lineitem_id { get; set; }
        public int product_id { get; set; }
        public int lineitem_quantity { get; set; }
        public int invoice_id { get; set; }
    
        public virtual tblSales_Invoice tblSales_Invoice { get; set; }
    }
}
