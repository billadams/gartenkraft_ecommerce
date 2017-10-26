namespace Gartenkraft_Admin.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    [MetadataType(typeof(tblCategoryMetaData))]
    public partial class tblProduct_Category
    {
    }
    public class tblCategoryMetaData
    {
        [DisplayName("Category ID")]
        public int category_id { get; set; }
        [DisplayName("Product Line ID")]
        public int category_product_line_id { get; set; }
        [DisplayName("Category Image ID")]
        public Nullable<int> category_image_id { get; set; }
        [DisplayName("Category Name")]
        public string category_name { get; set; }
        [DisplayName("Soft Delete Status")]
        public Nullable<bool> soft_delete { get; set; }
        [DisplayName("Visible Status")]
        public Nullable<bool> is_visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct> tblProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct_Category_Image> tblProduct_Category_Image { get; set; }
        public virtual tblProduct_Line tblProduct_Line { get; set; }
    }
}