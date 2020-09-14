using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teams.Domain;
using Teams.Data;

namespace Teams.Controllers
{
    public class SingleSelectionQuestionController : Controller
    {
        private ApplicationDbContext DbContext { get; set; }
        public SingleSelectionQuestionController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowQuestion(Guid id)
        {
            var options = new List<QuestionOption> { new QuestionOption(5, "the first option", false),
                        new QuestionOption(6, "the second option", false),
                        new QuestionOption(7, "the third option", true)};
            var temp = new SingleSelectionQuestion("the question", options);

            //var question = DbContext.SingleSelectionQuestions.FirstOrDefault(i => i.Id == id);
            //question.InitOptionList(options);

            return View(temp);
        }
    }
}
