using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebInkLibrary.Utils.SerialNumber
{
    public static class SerialNumberFeHelper
    {
        
        public static MvcHtmlString SerialNumber_DropdownList(this System.Web.Mvc.HtmlHelper helper ,string name, int recordId, int srno, IEnumerable<SelectListItem> list)
        {
            //This method in turns calls below overload.
            return SerialNumber_DropdownList(helper, name, recordId,srno, list,  null);
        }
 
        //This overload is extension method accepts "htmlAttributes" as parameters.
        public static MvcHtmlString SerialNumber_DropdownList(this System.Web.Mvc.HtmlHelper helper, string name, int recordId, int srno,IEnumerable<SelectListItem> list, object htmlAttributes)
        {
            var dropdown = new TagBuilder("select");
            
            dropdown.Attributes.Add("name", name);
            dropdown.Attributes.Add("id", name);

            //Attribute to used in ajax call
            dropdown.Attributes.Add("data-recordId",recordId.ToString(CultureInfo.InvariantCulture)); 
            
            var options = new StringBuilder();
            
            var selectedItem = srno;

            //Iterated over the IEnumerable list.
            foreach (var item in list)
            {
                var selected = item.Text == selectedItem.ToString(CultureInfo.InvariantCulture) ? "selected " : "";
                options = options.Append("<option value='" + item.Value +"' "+ selected +  ">" + item.Text + "</option>");
            }
            
            //assigned all the options to the dropdown using innerHTML property.
            dropdown.InnerHtml = options.ToString();
            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
    }
}
