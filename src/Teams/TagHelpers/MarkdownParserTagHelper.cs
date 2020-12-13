using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.TagHelpers
{
    public class MarkdownParserTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml(new HtmlString
                (
                $"<form>" +
                $"<input type=\"button\" value='Parse to Markdown' class='btn btn-light' onclick=\"getMarkdownQuestion(document.getElementById('questionInput').value)\"" +
                $"</form>" +
                $"<div id=\"OutQuestion\"></div>"
                ));
        }
    }
}
