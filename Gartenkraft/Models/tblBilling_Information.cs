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
    
    public partial class tblBilling_Information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBilling_Information()
        {
            this.tblSales_Invoice = new HashSet<tblSales_Invoice>();
        }
    
        public int billing_id { get; set; }
        public string customer_id { get; set; }
        public string billing_address1 { get; set; }
        public string billing_address2 { get; set; }
        public string billing_city { get; set; }
        public string billing_state { get; set; }
        public string billing_zip { get; set; }
        public string billing_zip4 { get; set; }
        public string billing_country { get; set; }
        public string billing_first_name { get; set; }
        public string billing_last_name { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSales_Invoice> tblSales_Invoice { get; set; }
    }
}
