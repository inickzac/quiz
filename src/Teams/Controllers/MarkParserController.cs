using Microsoft.AspNetCore.Mvc;
using Westwind.AspNetCore.Markdown;

namespace Teams.Controllers
{
    public class MarkParserController : Controller
    {
        [HttpPost]
        public JsonResult GetMarkdownQuestion(string Quest)
        {
            string html = Markdown.Parse(Quest);
            return Json(html);
        }
    }
}
