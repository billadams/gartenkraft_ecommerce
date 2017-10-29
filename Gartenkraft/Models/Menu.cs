using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class Menu
    {
        private List<vwProduct_Line> productLines { get; set; }
        private List<vwCategory> productCategories { get; set; }
    }
}