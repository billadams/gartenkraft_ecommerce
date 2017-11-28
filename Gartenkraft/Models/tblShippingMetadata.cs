using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(tblShippingMetadata))]
    public partial class tblShipping { }

    public class tblShippingMetadata
    {
        [Required]
        [Display(Name = "Address")]
        public string shipping_address1 { get; set; }

        [Required]
        [Display(Name = "")]
        public string shipping_address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string shipping_city { get; set; }

        [Required]
        [Display(Name = "State")]
        public string shipping_state { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string shipping_zip { get; set; }

        [Required]
        [Display(Name = "")]
        public string shipping_zip4 { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string shipping_country { get; set; }
    }
}