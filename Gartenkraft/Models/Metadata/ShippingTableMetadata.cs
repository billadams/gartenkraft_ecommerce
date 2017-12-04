using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(ShippingTableMetadata))]
    public partial class tblShipping { }

    public class ShippingTableMetadata
    {
        [Required(ErrorMessage = "Please enter your First Name here.")]
        [Display(Name = "First Name")]
        public string shipping_first_name { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name here.")]
        [Display(Name = "Last Name")]
        public string shipping_last_name { get; set; }

        [Required(ErrorMessage = "Please enter your Address here.")]
        [Display(Name = "Street Address")]
        public string shipping_address1 { get; set; }

        [Display(Name = "")]
        public string shipping_address2 { get; set; }

        [Required(ErrorMessage = "Please enter your City here.")]
        [Display(Name = "City")]
        public string shipping_city { get; set; }

        [Required(ErrorMessage = "Please enter your state here.")]
        [Display(Name = "State")]
        public string shipping_state { get; set; }

        [Required(ErrorMessage = "Please enter your Zip Code here.")]
        [Display(Name = "Zip Code")]
        public string shipping_zip { get; set; }

        [Display(Name = "")]
        public string shipping_zip4 { get; set; }

        [Required(ErrorMessage = "Please enter your Country here.")]
        [Display(Name = "Country")]
        public string shipping_country { get; set; }
    }
}