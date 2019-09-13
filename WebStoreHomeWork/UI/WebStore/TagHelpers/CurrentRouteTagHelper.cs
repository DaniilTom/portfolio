using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = "current-route")]
    public class CurrentRouteTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        private IDictionary<string, string> _RouteValues;

        [HtmlAttributeName("asp-all-route", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues
        {
            get => _RouteValues ?? (_RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            set => _RouteValues = value;
        }

        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if(RouteValues.ContainsKey("page"))
            {
                string current_page = ViewContext.HttpContext.Request.Query["page"];
                if (current_page == _RouteValues["page"])
                {
                    output.Attributes.Add("class", "current-route");
                }
                return;
            }

            string current_controller = ViewContext.RouteData.Values["Controller"].ToString();
            string current_action = ViewContext.RouteData.Values["Action"].ToString();

            var comparison = StringComparison.CurrentCultureIgnoreCase;

            if (string.Equals(Controller, current_controller, comparison) && string.Equals(Action, current_action, comparison))
            {
                var class_attr = output.Attributes.FirstOrDefault(a => a.Name == "class");
                if (class_attr is null)
                {
                    output.Attributes.Add("class","current-route");
                }
                else
                {
                    output.Attributes.SetAttribute("class", class_attr.Value + " " + "current-route");
                }
            }
        }
    }
}
