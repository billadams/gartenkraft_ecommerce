using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(tblBilling_InformationMetadata))]
    public partial class tblBilling_Information { }

    public class tblBilling_InformationMetadata
    {
        [Required]
        [Display(Name = "Address")]
        public string billing_address1 { get; set; }

        [Required]
        [Display(Name = "")]
        public string billing_address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string billing_city { get; set; }

        [Required]
        [Display(Name = "State")]
        public string billing_state { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string billing_zip { get; set; }

        [Required]
        [Display(Name = "")]
        public string billing_zip4 { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string billing_country { get; set; }
    }
}