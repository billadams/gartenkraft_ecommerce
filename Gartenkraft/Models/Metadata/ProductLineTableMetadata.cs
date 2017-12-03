namespace Gartenkraft.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(ProductLineTableMetadata))]
    public partial class tblProduct_Line { }

    public class ProductLineTableMetadata
    {
        [Display(Name = "Product Line Name")]
        public string product_line_name { get; set; }

        [Display(Name = "Date Added")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime date_added { get; set; }

        [Display(Name = "Soft Delete Status")]
        public bool soft_delete { get; set; }

        [Display(Name = "Visible Status")]
        public bool is_visible { get; set; }
    }
}