namespace Gartenkraft_Admin.Models
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
        public decimal product_unit_cost { get; set; }
        [DisplayName("Unit Price")]
        public decimal product_unit_price { get; set; }
        [DisplayName("Category ID")]
        public int product_category_id { get; set; }
        [DisplayName("Product Line ID")]
        public int product_line_id { get; set; }
        [DisplayName("Weight")]
        public decimal product_weight { get; set; }
        [DisplayName("Date Added")]
        public System.DateTime product_date_added { get; set; }
        [DisplayName("Soft Delete Status")]
        public Nullable<bool> soft_delete { get; set; }
        [DisplayName("Visible Status")]
        public Nullable<bool> is_visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFeature_Product> tblFeature_Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblInventory> tblInventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct_Image> tblProduct_Image { get; set; }
        public virtual tblProduct_Line tblProduct_Line { get; set; }
        public virtual tblProduct_Category tblProduct_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSales_Invoice_Lineitem> tblSales_Invoice_Lineitem { get; set; }
    }
}