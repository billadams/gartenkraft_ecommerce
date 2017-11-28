using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Gartenkraft.Models
{
    public class BillingInfo
    {
        public int BillingID { get; set; }
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter your Address here.")]
        [DisplayName("Street Address")]
        public string BillingAddress { get; set; }

        [DisplayName("Apt #, Floor, etc. (Optional)")]
        public string BillingAddress2 { get; set; } = "";
        [Required(ErrorMessage = "Please enter your City here.")]
        [DisplayName("City")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Please enter your state here.")]
        [DisplayName("State")]
        public string BillingState { get; set; }

        [Required(ErrorMessage = "Please enter your Zip Code here.")]
        [DisplayName("Zip Code")]
        public string BillingZip { get; set; } = "";
        [DisplayName("Zip4 (Optional)")]
        public string BillingZip4 { get; set; }
        [Required(ErrorMessage = "Please enter your Country here.")]
        [DisplayName("Country")]
        public string BillingCountry { get; set; }

    }
}