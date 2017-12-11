using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(BillingTableMetadata))]
    public partial class tblBilling_Information { }

    public class BillingTableMetadata
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        [Display(Name = "First Name")]
        public string billing_first_name { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        public string billing_last_name { get; set; }

        [Required(ErrorMessage = "Please enter your address.")]
        [Display(Name = "Street Address")]
        public string billing_address1 { get; set; }

        [Required(ErrorMessage = "Please enter your city.")]
        [Display(Name = "City")]
        public string billing_city { get; set; }

        [Required(ErrorMessage = "Please enter your state.")]
        [Display(Name = "State")]
        public string billing_state { get; set; }

        [Required(ErrorMessage = "Please enter your zip code.")]
        [Display(Name = "Zip Code")]
        public string billing_zip { get; set; }

        [Required(ErrorMessage = "Please enter your country.")]
        [Display(Name = "Country")]
        public string billing_country { get; set; }
    }
}