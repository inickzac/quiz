using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;

namespace EpamTesting.Controllers
{
    public class OpenAnswerQuestionController : Controller
    {

        ApplicationDbContext context;
        static int idQuestion;

        public OpenAnswerQuestionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Question(int? id)
        {

            int countQuestions = context.OpenAnswerQuestions.Count();

            ViewData["id"] = id;

            if (id == null || id < 1 || id > countQuestions)
            {
                return NotFound("Неверынй ввод ID");

            }

            idQuestion = (int)id - 1;

            IEnumerable<OpenAnswerQuestion>
              lin_s = context.OpenAnswerQuestions.Skip(idQuestion).Take(1).ToList();
            string question = lin_s.ElementAt(0).TextQuestion;


            ViewData["question"] = question;

            return View();
        }


        public IActionResult Answer(string answer)
        {
            IEnumerable<OpenAnswerQuestion>
              lin_ss = context.OpenAnswerQuestions.Skip(idQuestion).Take(1).ToList();
            string question = lin_ss.ElementAt(0).TextQuestion;


            IEnumerable<OpenAnswerQuestion>
            lin_s = context.OpenAnswerQuestions.Skip(idQuestion).Take(1).ToList();
            string answerCorrect = lin_s.ElementAt(0).TextAnswer;


            ViewData["question"] = question;
            ViewData["id"] = idQuestion + 1;



            ViewBag.Answer = answer;


            if (answer == answerCorrect)
            {
                ViewData["color"] = "green";
            }
            else
            {
                ViewData["color"] = "red";
            }

            return View();
        }

    }
}