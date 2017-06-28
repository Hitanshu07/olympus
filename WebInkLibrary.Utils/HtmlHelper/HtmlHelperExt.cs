using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace WebInkLibrary.Utils.HtmlHelper
{
    public static class HtmlHelperExt
    {
        private enum TextType
        {
            TextBox,
            TextArea
        }

        public static MvcHtmlString CharactersRemainingTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return CharactersRemainingTextBoxFor(html, expression, new RouteValueDictionary());
        }

        public static MvcHtmlString CharactersRemainingTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return CharactersRemainingTextBoxFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString CharactersRemainingTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            return CharactersRemainingTextFor(html, expression, htmlAttributes, TextType.TextBox);
        }

        public static MvcHtmlString CharactersRemainingTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return CharactersRemainingTextAreaFor(html, expression, new RouteValueDictionary());
        }

        public static MvcHtmlString CharactersRemainingTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return CharactersRemainingTextAreaFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString CharactersRemainingTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            return CharactersRemainingTextFor(html, expression, htmlAttributes, TextType.TextArea);
        }

        private static MvcHtmlString CharactersRemainingTextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, TextType textType)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            //Attempt to get the StringLengthAttribute from the Model
            StringLengthAttribute stringLengthAttribute = memberExpression.Member.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault() as StringLengthAttribute;

            TagBuilder textboxTag;
            if (textType == TextType.TextBox)
            {
                textboxTag = new TagBuilder("input");
            }
            else
            {
                textboxTag = new TagBuilder("textarea");
            }

            textboxTag.MergeAttributes(htmlAttributes);

            if (textType == TextType.TextBox)
            {
                textboxTag.MergeAttribute("type", "text");
            }

            //If the Id attribute is not set the set it to a new Guid
            if (!textboxTag.Attributes.ContainsKey("id"))
            {
                textboxTag.Attributes.Add("id", Guid.NewGuid().ToString());
            }

            //Set the name attribute to allow the binding on the POST
            //from msdn htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var name = ExpressionHelper.GetExpressionText(expression);
            textboxTag.MergeAttribute("name", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name));


            //Get the text to display in the textbox
            string text = Convert.ToString(ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model);

            if (textType == TextType.TextBox)
            {
                textboxTag.MergeAttribute("value", text);
            }
            else
            {
                textboxTag.SetInnerText(text);
            }

            if (stringLengthAttribute != null && stringLengthAttribute.MaximumLength > 0)
            {
                //Text area ignores maxlength but required to work out remaining characters
                textboxTag.Attributes.Add("maxlength", Convert.ToString(stringLengthAttribute.MaximumLength));
                textboxTag.Attributes.Add("onkeyup", string.Format("if (this.value.length > this.getAttribute('maxlength')) this.value = this.value.substring(0, this.getAttribute('maxlength')); document.getElementById('{0}').innerHTML = (this.getAttribute('maxlength') - (this.value.length)) + ' characters remaining';", string.Concat(textboxTag.Attributes["id"], "_remaining")));

                if (textType == TextType.TextArea)
                {
                    //Use JavaScript to restrict the number of characters
                    textboxTag.Attributes.Add("onkeypress", "key = window.event ? event.keyCode : event.which; if((this.value.length >= this.getAttribute('maxlength')) && (key == 32 || key == 13 || key > 47)) { if (window.event) { window.event.returnValue = null; } else { event.cancelDefault; return false; } }");
                }

                //Create the tag to display the number of remaining characters
                TagBuilder spanTag = new TagBuilder("span");
                spanTag.Attributes.Add("id", string.Concat(textboxTag.Attributes["id"], "_remaining"));

                //If a class attribute is provided also give this class to the span tag
                if (htmlAttributes.ContainsKey("class"))
                {
                    spanTag.AddCssClass(Convert.ToString(htmlAttributes["class"]));
                }
                spanTag.InnerHtml = string.Format("{0} characters remaining", stringLengthAttribute.MaximumLength - text.Length);

                return MvcHtmlString.Create(string.Concat(textboxTag.ToString(TagRenderMode.Normal), spanTag.ToString(TagRenderMode.Normal)));
            }

            return MvcHtmlString.Create(textboxTag.ToString(TagRenderMode.Normal));
        }
    }
}