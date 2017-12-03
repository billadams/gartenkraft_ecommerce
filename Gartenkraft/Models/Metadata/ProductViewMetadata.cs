using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(ProductViewMetadata))]
    public partial class vwProduct
    {
        [NotMapped]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [NotMapped]
        public List<tblProduct_Image> ProductImages { get; set; }

        [NotMapped]
        public List<tblProduct_Option> ProductOptions { get; set; }

        [NotMapped]
        public int SelectedOptionID { get; set; }

        [NotMapped]
        public vwProduct_Option SelectedOption { get; set; }

        [NotMapped]
        [Display(Name = "Price")]
        public string PriceRange { get; private set; }

        [NotMapped]
        public decimal Price { get; set; }

        public void SetOption()
        {
            var db = new GartenkraftEntities();

            // setting option for simple product
            if (this.is_custom_product == false)
            {
                this.SelectedOption = db.vwProduct_Option.FirstOrDefault(productOption => productOption.product_id == this.product_id);
            }
            // else custom product
            else
            {
                this.SelectedOption = db.vwProduct_Option.FirstOrDefault(productOption => productOption.option_id == this.SelectedOptionID);
            }

            db.Dispose();
        }

        public void SetPriceRange()
        {
            var db = new GartenkraftEntities();
            this.ProductImages = db.tblProduct_Image.Where(pi => pi.product_id == this.product_id).ToList();
            this.ProductOptions = db.tblProduct_Option.Where(o => o.product_id == this.product_id).ToList();
            db.Dispose();

            if (this.ProductOptions.Count > 1 && this.ProductOptions != null)
            {
                decimal lowestPrice = 0m, highestPrice = 0m;
                foreach (var i in this.ProductOptions)
                {
                    // initialize lowestprice
                    if (lowestPrice == 0) { lowestPrice = i.unit_price; }

                    if (i.unit_price < lowestPrice) { lowestPrice = i.unit_price; }
                    if (i.unit_price > highestPrice) { highestPrice = i.unit_price; }
                }

                this.PriceRange = lowestPrice.ToString("c") + " - " + highestPrice.ToString("c");
            }
            else if (this.ProductOptions.Count == 1)
            {
                string price = "";
                foreach (var i in this.ProductOptions) { price = i.unit_price.ToString("c"); }
                this.PriceRange = price;
            }
        }

        public void SetPrice()
        {
            SetOption();
            this.Price = this.SelectedOption.unit_price;
        }
    }

    public class ProductViewMetadata
    {
        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Display(Name = "Short Description")]
        public string product_short_description { get; set; }

        [Display(Name = "Long Description")]
        public string product_long_description { get; set; }

        [Display(Name = "Category Name")]
        public string category_name { get; set; }

        [Display(Name = "Product Line Name")]
        public string product_line_name { get; set; }

    }
}