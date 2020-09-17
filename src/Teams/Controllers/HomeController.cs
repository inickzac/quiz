using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Teams.Data;
using Teams.Domain;
using Teams.Models;

namespace Teams.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        static int CountQuestions = -1;
        static int IdQuestion = 0;

        public HomeController(ApplicationDbContext _db)
        {

            db = _db;
            if (CountQuestions == -1)
            {
                CountQuestions = 0;
                CountQuestions = db.SingleSelectionQuestions.Count();
            }
        }

        public void Delete()
        {
            while (true)
            {
                Question qu = db.Questions.FirstOrDefault();
                if (qu != null)
                {
                    db.Questions.Remove(qu);
                    db.SaveChanges();
                }
                else
                {
                    break;
                }
            }
            CountQuestions = 0;
            IdQuestion = 0;
        }



        public IActionResult Index()
        {
            ViewData["LastNumberQuestion"] = CountQuestions;
            ViewData["Id"] = IdQuestion + 1;
            ViewData["Error"] = "";
            return View();
        }



        public IActionResult Quest(int? IdQuest)
        {
            if (IdQuest < 1 || IdQuest > CountQuestions || IdQuest == null)
            {
                if (IdQuest == null) IdQuest = IdQuest + 1;

                ViewData["LastNumberQuestion"] = CountQuestions;
                ViewData["Id"] = IdQuestion + 1;
                ViewData["Error"] = "Incorrect!";

                return View("/Views/Home/Index.cshtml");
            }

            IdQuestion = (int)IdQuest - 1;
            IEnumerable<SingleSelectionQuestion> linQue = db.SingleSelectionQuestions.Skip(IdQuestion).Take(1).ToList();
            string question = linQue.ElementAt(0).Text;

            ViewData["Id"] = IdQuest;
            ViewData["question"] = question;
            return View();
        }


        public IActionResult Check(string Answer)
        {
            if (Answer == null) Answer = "";

            IEnumerable<MultipleAnswerQuestion> linAns =
                db.MultipleAnswerQuestions.Skip(IdQuestion).Take(1).ToList();
            string answer = linAns.ElementAt(0).Text;

            IEnumerable<SingleSelectionQuestion> linQue =
                db.SingleSelectionQuestions.Skip(IdQuestion).Take(1).ToList();
            string question = linQue.ElementAt(0).Text;



            ViewData["Id"] = IdQuestion + 1;
            ViewData["Question"] = question;
            ViewData["Answer"] = Answer;

            if (String.Equals(answer, Answer))
            {
                ViewData["Color"] = "green";
            }
            else
            {
                ViewData["Color"] = "red";
            }

            return View();
        }

    }
}
