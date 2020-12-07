using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.TagHelpers
{
    public class MarkdownHtmlParserTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml(new HtmlString
                (
                $"<form action='InfoMarkdown' controller='OpenQuestionCreate' class='Unique-Name-class'>" +
                $"<input type = 'text' id='InfoMarkdown' name='questionMarkdown' hidden />" +
                $"<input type = 'submit' value='Parse to Markdown' class='btn btn-light' />" +
                $"</form>"
                ));
        }
    }
    public class MarkdownScriptParserTagHelper : TagHelper
    {
        public string IdTextArea { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml(new HtmlString(
                "<script>$('.Unique-Name-class').submit(function() { $('#InfoMarkdown').val($('#" + IdTextArea + "').val()); return true; });</script>"            
                ));
        }
    }
}
