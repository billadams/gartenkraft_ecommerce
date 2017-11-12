using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    public partial class invoice_lineitem : tblSales_Invoice_Lineitem
    {
        private vwProduct product;

        public decimal LineTotal { get; set; }

        public void Set_lineitem_quantity(int qty)
        {
            base.lineitem_quantity = qty;
            if (this.product != null)
            {
                SetLineTotal();
            }            
        }

        public void SetProduct(vwProduct p)
        {
            this.product = p;
            base.product_id = p.product_id;
        }

        public vwProduct GetProduct()
        {
            return this.product;
        }

        private void SetLineTotal()
        {
            this.LineTotal = this.product.SelectedOption.unit_price * base.lineitem_quantity;
        }
    }
}