using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(AspNetUsersMetadata))]
    public partial class AspNetUser { }

    public class AspNetUsersMetadata
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Address")]
        public string Address1 { get; set; }
        
        [Display(Name = "")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string Zip { get; set; }

        [Display(Name = "")]
        public string Zip4 { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "")]
        public string ImageFileName { get; set; }
    }
}