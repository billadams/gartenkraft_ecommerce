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
    
    public partial class vwCategory
    {
        public string category_name { get; set; }
        public int category_id { get; set; }
        public int category_product_line_id { get; set; }
        public Nullable<bool> soft_delete { get; set; }
        public Nullable<bool> is_visible { get; set; }
    }
}
