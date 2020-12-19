using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.ViewComponents
{
    public class MarkdownScriptFormViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string questionId)
        {
            ViewData["TextQuestionId"] = questionId;
            return  View();
        }

    }
}
