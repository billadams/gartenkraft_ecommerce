using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class Shipping
    {

        public int ShippingID { get; set; }
        
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter your First Name here.")]
        [DisplayName("First Name")]
        public string ShippingFirstName { get; set; }
        [Required(ErrorMessage = "Please enter your Last Name here.")]
        [DisplayName("Last Name")]
        public string ShippingLastName { get; set; }
        [Required(ErrorMessage = "Please enter your Address here.")]
        [DisplayName("Address")]
        public string ShippingAddress { get; set; }

        [DisplayName("Address 2")]
        public string ShippingAddress2 { get; set; } = "";
        [Required(ErrorMessage = "Please enter your City here.")]
        [DisplayName("City")]
        public string ShippingCity { get; set; }
        [Required(ErrorMessage = "Please enter your state here.")]
        [DisplayName("State")]
        public string ShippingState { get; set; }
        [Required(ErrorMessage = "Please enter your Zip Code here.")]
        [DisplayName("Zip Code")]
        public string ShippingZip { get; set; }

        [DisplayName("Zip 4")]
        public string ShippingZip4 { get; set; } = "";
        [Required(ErrorMessage = "Please enter your Country here.")]
        [DisplayName("Country")]
        public string ShippingCountry { get; set; }
    }
}