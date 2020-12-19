using Microsoft.AspNetCore.Mvc;
using Westwind.AspNetCore.Markdown;

namespace Teams.Controllers
{
    public class MarkParserController : Controller
    {
        [HttpPost]
        public JsonResult GetMarkdownQuestion(string question)
        {
            string html = Markdown.Parse(question);
            return Json(html);
        }
    }
}
