using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    public class Cart
    {
        public const decimal TAX = 0.07m;
        private List<tblSales_Invoice_Lineitem> cartItems;

        public List<tblSales_Invoice_Lineitem> CartItems
        {
            get { return this.cartItems; }
            set { this.cartItems = value; }
        }

        [Display(Name = "Cart Owner")]
        public AspNetUser CartOwner { get; set; }
        public tblSales_Invoice Invoice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Subtotal { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TaxAmount { get; private set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; private set; }

        public Cart() { cartItems = new List<tblSales_Invoice_Lineitem>(); }

        public void AddItem(tblSales_Invoice_Lineitem li)
        {
            bool duplicate = false;
            foreach (tblSales_Invoice_Lineitem item in this.cartItems)
            {
                if (li.GetProduct().product_id == item.product_id)
                {
                    duplicate = true;
                    item.lineitem_quantity += 1;
                }
            }
            if (!duplicate)
            {
                this.cartItems.Add(li);
            }
            CalculateTotal();

        }

        public void UpdateItem(tblSales_Invoice_Lineitem li)
        {
            foreach (tblSales_Invoice_Lineitem item in this.cartItems)
            {
                if (li.GetProduct().product_id == item.product_id)
                {
                    item.lineitem_quantity = li.lineitem_quantity;
                }
            }
            CalculateTotal();
        }

        public void RemoveItem(tblSales_Invoice_Lineitem li)
        {
            foreach (tblSales_Invoice_Lineitem item in this.cartItems)
            {
                if (li.GetProduct().product_id == item.product_id)
                {
                    li = item;
                }
            }
            this.cartItems.Remove(li);
            CalculateTotal();
        }

        public void ClearCart()
        {
            this.CartItems.Clear();
            this.Total = 0m;
        }

        private void SetSubtotal()
        {
            this.Subtotal = 0m;
            foreach (tblSales_Invoice_Lineitem li in this.CartItems)
            {
                this.Subtotal += li.GetLineTotal();
            }
        }

        private void SetTaxAmount()
        {
            this.TaxAmount = this.Subtotal * TAX;
        }

        private void CalculateTotal()
        {
            SetSubtotal();
            SetTaxAmount();
            this.Total = this.Subtotal + this.TaxAmount;
        }

    }
}