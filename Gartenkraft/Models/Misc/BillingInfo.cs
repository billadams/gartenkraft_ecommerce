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
        [Required(ErrorMessage = "Please enter your First Name here.")]
        [DisplayName("First Name")]
        public string BillingFirstName { get; set; }
        [Required(ErrorMessage = "Please enter your Last Name here.")]
        [DisplayName("Last Name")]
        public string BillingLastName { get; set; }
        [Required(ErrorMessage = "Please enter your Address here.")]
        [DisplayName("Address")]
        public string BillingAddress { get; set; }

        [DisplayName("Address 2")]
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
        [DisplayName("Zip 4")]
        public string BillingZip4 { get; set; }
        [Required(ErrorMessage = "Please enter your Country here.")]
        [DisplayName("Country")]
        public string BillingCountry { get; set; }

    }
}