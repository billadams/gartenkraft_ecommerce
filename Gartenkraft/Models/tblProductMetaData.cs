namespace Gartenkraft.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    [MetadataType(typeof(tblProductMetaData))]

    public partial class tblProduct
    {
    }

    public class tblProductMetaData
    {
        [DisplayName("Product ID")]
        public int product_id { get; set; }
        [DisplayName("Name")]
        public string product_name { get; set; }
        [DisplayName("Short Desc")]
        public string product_short_description { get; set; }
        [DisplayName("Long Desc")]
        public string product_long_description { get; set; }
        [DisplayName("Unit Cost")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal product_unit_cost { get; set; }
        [DisplayName("Unit Price")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal product_unit_price { get; set; }
        [DisplayName("Category ID")]
        public int product_category_id { get; set; }
        [DisplayName("Product Line ID")]
        public int product_line_id { get; set; }
        [DisplayName("Weight")]
        public decimal product_weight { get; set; }
        [DisplayName("Date Added")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime product_date_added { get; set; }
        [DisplayName("Soft Delete Status")]
        public Nullable<bool> soft_delete { get; set; }
        [DisplayName("Visible Status")]
        public Nullable<bool> is_visible { get; set; }
    }
}