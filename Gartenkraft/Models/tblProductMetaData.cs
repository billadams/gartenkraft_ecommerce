
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Gartenkraft.Models
{
   
    [MetadataType(typeof(tblProductMetaData))]

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

        [DisplayName("Category ID")]
        public int product_category_id { get; set; }

        [DisplayName("Product Line ID")]
        public int product_line_id { get; set; }

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