namespace Gartenkraft_Admin.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    [MetadataType(typeof(tblProduct_LineMetaData))]
    public partial class tblProduct_Line
    {
    }
    public class tblProduct_LineMetaData
    {
        [DisplayName("Product Line ID")]
        public int product_line_id { get; set; }
        [DisplayName("Product Line Name")]
        public string product_line_name { get; set; }
        [DisplayName("Date Added")]
        public System.DateTime date_added { get; set; }
        [DisplayName("Soft Delete Status")]
        public bool soft_delete { get; set; }
        [DisplayName("Visible Status")]
        public bool is_visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct> tblProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct_Category> tblProduct_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProduct_Line_Image> tblProduct_Line_Image { get; set; }
    }
}