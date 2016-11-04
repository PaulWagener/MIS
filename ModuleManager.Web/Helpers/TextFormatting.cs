using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuleManager.Web.Helpers
{
    public static class TextFormatting
    {
        /// <summary>
        /// <para>Formats a longer text that may contain line breaks
        /// such that the intent of the author is retained.</para>
        /// <para>This method converts single new lines to line breaks and
        /// double new lines to paragraphs</para>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString FormatLongText(this HtmlHelper htmlHelper,
            string text)
        {
            return MvcHtmlString.Create(
                text
                    // split into paragraphs
                    .Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                    // wrap each paragraph in a p-tag
                    .Select(paragraph => new TagBuilder("p")
                    {
                        InnerHtml = paragraph.Replace("\n", "<br>")
                    })
                    .Select(tb => tb.ToString())
                    // concat paragraphs
                    .Aggregate("", (total, part) => total + part)

            );
        }
    }
}