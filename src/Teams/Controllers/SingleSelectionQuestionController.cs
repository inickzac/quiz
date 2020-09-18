using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teams.Domain;
using Teams.Data;
using Microsoft.EntityFrameworkCore;

namespace Teams.Controllers
{
    public class SingleSelectionQuestionController : Controller
    {
        private IApplicationDbContext DbContext { get; set; }
        public SingleSelectionQuestionController(IApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowQuestion(Guid id)
        {
            var question = DbContext.SingleSelectionQuestions.Include(q => q.OptionList)
                .FirstOrDefault(i => i.Id == id);
            return View(question);
        }
        [HttpGet]
        public JsonResult FindAnswer(Guid idQuestion)
        {
            var question = DbContext.SingleSelectionQuestions.Include(q => q.OptionList)
                .FirstOrDefault(i => i.Id == idQuestion);
            var answer = question.OptionList.FirstOrDefault(i => i.IsAnswer == true);
            return Json(answer);
        }
    }
}
