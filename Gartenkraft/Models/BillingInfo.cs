using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class BillingInfo
    {
        public int BillingID { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingZip4 { get; set; }

        public string BillingCountry { get; set; }
    }
}