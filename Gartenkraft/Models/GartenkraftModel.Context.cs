﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gartenkraft.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GartenkraftEntities : DbContext
    {
        public GartenkraftEntities()
            : base("name=GartenkraftEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<tblBilling_Information> tblBilling_Information { get; set; }
        public virtual DbSet<tblFeature_Product> tblFeature_Product { get; set; }
        public virtual DbSet<tblInventory> tblInventories { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblProduct_Category> tblProduct_Category { get; set; }
        public virtual DbSet<tblProduct_Category_Image> tblProduct_Category_Image { get; set; }
        public virtual DbSet<tblProduct_Image> tblProduct_Image { get; set; }
        public virtual DbSet<tblProduct_Line> tblProduct_Line { get; set; }
        public virtual DbSet<tblProduct_Line_Image> tblProduct_Line_Image { get; set; }
        public virtual DbSet<tblProduct_Option> tblProduct_Option { get; set; }
        public virtual DbSet<tblSales_Invoice> tblSales_Invoice { get; set; }
        public virtual DbSet<tblSales_Invoice_Lineitem> tblSales_Invoice_Lineitem { get; set; }
        public virtual DbSet<tblShipping> tblShippings { get; set; }
        public virtual DbSet<vwCategory> vwCategories { get; set; }
        public virtual DbSet<vwCheckout> vwCheckouts { get; set; }
        public virtual DbSet<vwFeaturedProduct> vwFeaturedProducts { get; set; }
        public virtual DbSet<vwInvoice> vwInvoices { get; set; }
        public virtual DbSet<vwInvoice_Lineitem> vwInvoice_Lineitem { get; set; }
        public virtual DbSet<vwProduct> vwProducts { get; set; }
        public virtual DbSet<vwProduct_Line> vwProduct_Line { get; set; }
        public virtual DbSet<vwProduct_Option> vwProduct_Option { get; set; }
    }
}
