//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gartenkraft_Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class tblSales_Invoice_Lineitem
    {
        [Required]
        [DisplayName("ID")]
        public int lineitem_id { get; set; }
        [Required]
        
        [DisplayName("Product ID")]
        public int product_id { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int lineitem_quantity { get; set; }
        [Required]
        [DisplayName("Invoice ID")]
        public int invoice_id { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblSales_Invoice tblSales_Invoice { get; set; }
    }
}
