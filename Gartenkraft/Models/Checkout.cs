using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class Checkout
    {
        //could import
        public string ShippingID  { get; set; }
        public string CustomerID { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingZip4 { get; set; }
        public string ShippingCountry { get; set; }



    }
}