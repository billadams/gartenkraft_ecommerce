﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gartenkraft.Models;

namespace Gartenkraft.ViewModels
{
    public class MenuModel
    {
        public List<ProductLineMenuModel> ProductLines { get; set; }

        public MenuModel()
        {
            // initialize List<>
            this.ProductLines = new List<ProductLineMenuModel>();

            // get vwProduct_Lines
            var vwProdLines = new GartenkraftEntities().vwProduct_Line.Where(pl => pl.is_visible == true).ToList();

            //assign to MenuModel
            foreach (var prodLine in vwProdLines)
            {
                this.ProductLines.Add(new ProductLineMenuModel(prodLine));
            }
        }
    }

    public class ProductLineMenuModel : vwProduct_Line
    {
        public List<vwCategory> Categories { get; private set; }

        public ProductLineMenuModel(vwProduct_Line prodLine)
        {
            // assign values
            this.product_line_id = prodLine.product_line_id;
            this.product_line_name = prodLine.product_line_name;
            this.is_visible = prodLine.is_visible;
            this.soft_delete = prodLine.soft_delete;

            // initialize List<>
            this.Categories = new List<vwCategory>();

            // get vwCategories
            this.Categories = new GartenkraftEntities().vwCategories.Where(c => c.category_product_line_id == prodLine.product_line_id && c.is_visible == true).ToList();
        } 
    }
}