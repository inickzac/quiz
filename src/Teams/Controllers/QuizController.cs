using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Domain;
using Teams.Models;

namespace Teams.Controllers
{
    public class QuizController : Controller
    {
        public QuizController(ApplicationDbContext db)
        {
            _db = db;
        }
        private readonly ApplicationDbContext _db;
        Guid id;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Guid Id)
        {
            id = Id;
            // test
            var a = new MultipleAnswerQuestion("Pick EU capitals", new List<Answer>() { new Answer("Paris", true), new Answer("Kyiv"), 
            new Answer("Wien", true), new Answer("Dresden")});
            a.Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4");
            //_db.MultipleAnswerQuestions.Add(a);
            //_db.SaveChanges();
            // delete between
            //a = MultipleAnswerQuestion.PickById(id, _db);
            var model = new MAQuestionModel()
            {
                Text = a.Text,
                Answers = a.answers,
                ID = a.Id.ToString()
            };
            return View("MAQForm", model);
        }
        public IActionResult MAQForm(string Id, int[] answer)
        {
            var question = new MultipleAnswerQuestion("Pick EU capitals", new List<Answer>() { new Answer("Paris", true), new Answer("Kyiv"),
            new Answer("Wien", true), new Answer("Dresden")});//MultipleAnswerQuestion.PickById(new Guid(Id), _db);
            bool b = true;
            for (int i = 0; i < question.answers.Count; i++)
            {
                if ((question.answers[i].isRight && !answer.Contains(i)) || (!question.answers[i].isRight && answer.Contains(i)))
                {
                    b = false;
                    break;
                }
            }
            var model = new MAQuestionModel()
            {
                ID = question.Id.ToString(),
                Text = question.Text,
                Answers = question.answers,
                ChosenOptions = new List<int>(answer)
            };
            return View(model);
        }
    }
}
