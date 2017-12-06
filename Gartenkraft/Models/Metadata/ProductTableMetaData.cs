
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Gartenkraft.Models
{
   
    [MetadataType(typeof(ProductTableMetadata))]
    public partial class tblProduct
    {
        [NotMapped]
        [Display(Name = "Price")]
        public string PriceRange { get; private set; }

        public void SetPriceRange()
        {
            if (this.tblProduct_Option.Count > 1 && this.tblProduct_Option != null)
            {
                decimal lowestPrice = 0m, highestPrice = 0m;
                foreach (var i in this.tblProduct_Option)
                {
                    // initialize lowestprice
                    if (lowestPrice == 0) { lowestPrice = i.unit_price; }

                    if (i.unit_price < lowestPrice) { lowestPrice = i.unit_price; }
                    if (i.unit_price > highestPrice) { highestPrice = i.unit_price; }
                }

                this.PriceRange = lowestPrice.ToString("c") + " - " + highestPrice.ToString("c");
            }
            else if (this.tblProduct_Option.Count == 1)
            {
                string price = "";
                foreach (var i in this.tblProduct_Option) { price = i.unit_price.ToString("c"); }
                this.PriceRange = price;
            }
        }
    }

    public class ProductTableMetadata
    {
        [DisplayName("Product ID")]
        public int product_id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string product_name { get; set; }

        [Required]
        [DisplayName("Short Desc")]
        public string product_short_description { get; set; }

        [Required]
        [DisplayName("Long Desc")]
        [DataType(DataType.MultilineText)]
        public string product_long_description { get; set; }

        [Required]
        [DisplayName("Category ID")]
        public int product_category_id { get; set; }

        [Required]
        [DisplayName("Date Added")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime product_date_added { get; set; }

        [DisplayName("Soft Delete Status")]
        public Nullable<bool> soft_delete { get; set; }

        [DisplayName("Visibility Status")]
        public Nullable<bool> is_visible { get; set; }

        [DisplayName("Custom Product")]
        public Nullable<bool> is_custom_product { get; set; }
    }
}