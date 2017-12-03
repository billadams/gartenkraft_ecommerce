using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gartenkraft.Models
{
    [MetadataType(typeof(ProductOptionTableMetadata))]
    public partial class tblProduct_Option { }

    public class ProductOptionTableMetadata
    {
        [Required]
        [DisplayName("Option Name")]
        public string title { get; set; }

        [Required]
        [DisplayName("Weight")]
        public decimal weight { get; set; }

        [Required]
        [DisplayName("Unit Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal unit_cost { get; set; }

        [Required]
        [DisplayName("Unit Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal unit_price { get; set; }
    }
}