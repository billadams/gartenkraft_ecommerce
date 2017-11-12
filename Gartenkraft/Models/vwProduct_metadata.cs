﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(vwProductMetadata))]
    public partial class vwProduct
    {
        [NotMapped]
        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        [NotMapped]
        public List<tblProduct_Image> ProductImages { get; set; }

        [NotMapped]
        public List<tblProduct_Option> Options { get; set; }

        [NotMapped]
        public int SelectedOptionID { get; set; }

        [NotMapped]
        public vwProduct_Option SelectedOption { get; set; }

        public void SetOption()
        {
            var db = new GartenkraftEntities();
            this.SelectedOption = db.vwProduct_Option.Where(po => po.option_id == this.SelectedOptionID).Single();
            db.Dispose();
        }

        [NotMapped]
        [Display(Name = "Price")]
        public string PriceRange { get; private set; }

        public void SetPriceRange()
        {
            var db = new GartenkraftEntities();
            this.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == this.product_id).ToList();
            this.Options = db.tblProduct_Option.Where(o => o.product_id == this.product_id).ToList();
            db.Dispose();
            if (this.Options.Count > 1 && this.Options != null)
            {
                decimal lowestPrice = 0m, highestPrice = 0m;
                foreach (var i in this.Options)
                {
                    // initialize lowestprice
                    if (lowestPrice == 0) { lowestPrice = i.unit_price; }

                    if (i.unit_price < lowestPrice) { lowestPrice = i.unit_price; }
                    if (i.unit_price > highestPrice) { highestPrice = i.unit_price; }
                }

                this.PriceRange = lowestPrice.ToString("c") + " - " + highestPrice.ToString("c");
            }
            else if (this.Options.Count == 1)
            {
                string price = "";
                foreach (var i in this.Options) { price = i.unit_price.ToString("c"); }
                this.PriceRange = price;
            }
        }
    }

    public class vwProductMetadata
    {
        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Display(Name = "Short Description")]
        public string product_short_description { get; set; }

        [Display(Name = "Long Description")]
        public string product_long_description { get; set; }

    }
}