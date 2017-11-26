using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Gartenkraft.Models
{
    public class XmlHelper
    {
        public static SelectList GetStates(HttpServerUtilityBase server, UrlHelper url)
        {
            var model = XDocument.Load(server.MapPath(url.Content("~/App_Data/states.xml")));
            IEnumerable<XElement> result = from c in model.Elements("states").Elements("state") select c;
            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "--- Please select a State ---",
                Value = "",
            });
            foreach (var xElement in result)
            {
                listItems.Add(new SelectListItem
                {
                    Text = xElement.Attribute("name").Value,
                    Value = xElement.Attribute("abbreviation").Value
                });
            }
            return new SelectList(listItems, "Value", "Text");
        }

        public static SelectList GetCountries(HttpServerUtilityBase server, UrlHelper url)
        {
            var model = XDocument.Load(server.MapPath(url.Content("~/App_Data/countries.xml")));
            IEnumerable<XElement> result = from c in model.Elements("countries").Elements("country") select c;
            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "--- Please select a Country ---",
                Value = "",
            });
            foreach (var xElement in result)
            {
                listItems.Add(new SelectListItem
                {
                    Text = xElement.Value,
                    Value = xElement.Value
                });
            }
            return new SelectList(listItems, "Value", "Text");
        }
    }
}