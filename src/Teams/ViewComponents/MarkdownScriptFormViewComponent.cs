using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.ViewComponents
{
    public class MarkdownScriptFormViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(string questId)
        {
            ViewData["TextQuestId"] = questId;
            return View();
        }

    }
}
