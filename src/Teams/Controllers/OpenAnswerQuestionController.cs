using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;

namespace Teams.Controllers
{
    public class OpenAnswerQuestionController : Controller
    {

        ApplicationDbContext context;

        public OpenAnswerQuestionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Question(Guid id)
        {
            OpenAnswerQuestion question = context.OpenAnswerQuestions.FirstOrDefault(x => x.Id == id);
                                
            if(question == null)
            {
                return NotFound("Не найден элемент по данному Id");
            }

            ViewData["id"] = id;
            ViewData["question"] = question.Text;

            return View();
        }


        public IActionResult Answer(string answer, Guid id)
        {
            OpenAnswerQuestion question = context.OpenAnswerQuestions.FirstOrDefault(x => x.Id == id);

            ViewData["question"] = question.Text;
            ViewBag.Answer = answer;           
            ViewData["color"] = question.CheckAnswer(answer);
            
            return View();
        }

    }
}