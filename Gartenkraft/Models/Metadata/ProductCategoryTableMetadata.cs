using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Gartenkraft.Models
{
    [MetadataType(typeof(ProductCategoryTableMetadata))]
    public partial class tblProduct_Category { }

    public class ProductCategoryTableMetadata
    {
        [Display(Name = "Category ID")]
        public int category_id { get; set; }

        [Display(Name = "Product Line ID")]
        public int category_product_line_id { get; set; }

        [Display(Name = "Category Name")]
        public string category_name { get; set; }

        [Display(Name = "Soft Deleted")]
        public bool soft_delete { get; set; }

        [Display(Name = "Is Visible")]
        public bool is_visible { get; set; }
    }
}