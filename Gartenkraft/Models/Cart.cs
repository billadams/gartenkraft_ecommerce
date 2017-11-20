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
        private List<invoice_lineitem> cartItems;

        public List<invoice_lineitem> CartItems
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

        public Cart() { cartItems = new List<invoice_lineitem>(); }

        public void AddItem(invoice_lineitem li)
        {
            bool duplicate = false;
            foreach (invoice_lineitem item in this.cartItems)
            {
                if (li.GetProduct().product_id == item.product_id && li.GetProduct().SelectedOptionID == item.GetProduct().SelectedOptionID)
                {
                    duplicate = true;
                    item.Set_lineitem_quantity(item.lineitem_quantity += 1);
                }
            }
            if (!duplicate)
            {
                this.cartItems.Add(li);
            }
            CalculateTotal();

        }

        public void UpdateItem(invoice_lineitem li)
        {
            this.cartItems.Where(ci => ci.product_id == li.product_id && ci.product_option_id == li.product_option_id).Single().Set_lineitem_quantity(li.lineitem_quantity);
            CalculateTotal();
        }

        public void RemoveItem(int productID)
        {
            this.cartItems.Remove(this.cartItems.Where(ci => ci.product_id == productID).Single());
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
            foreach (invoice_lineitem li in this.CartItems)
            {
                this.Subtotal += li.LineTotal;
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