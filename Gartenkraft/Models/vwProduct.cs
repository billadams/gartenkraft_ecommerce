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
    
    public partial class vwProduct
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_short_description { get; set; }
        public string product_long_description { get; set; }
        public decimal product_unit_price { get; set; }
        public int product_category_id { get; set; }
        public int product_line_id { get; set; }
        public decimal product_weight { get; set; }
        public Nullable<bool> is_visible { get; set; }
    }
}
