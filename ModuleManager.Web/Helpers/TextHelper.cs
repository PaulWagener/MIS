using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuleManager.Web.Helpers
{
    public static class TextHelper
    {
        /// <summary>
        /// Simple helper method that splits a text using a single string separator
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="separator">separator at which to split</param>
        /// <returns></returns>
        private static string[] SimpleSplit(this string text, string separator) =>
            text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Places text separated by two new lines into paragraphs and replaces the remaining
        /// new lines with br-tags
        /// </summary>
        /// <param name="h"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString FormatLongText(this HtmlHelper h, string text) =>
            MvcHtmlString.Create(
                text.SimpleSplit("\n\n")
                .Select(paragraphText => new TagBuilder("p")
                {
                    InnerHtml = h.Encode(paragraphText).Replace("\n", "<br>")
                })
                .Aggregate("", (total, pb) => total + pb.ToString())
            );
    }
}