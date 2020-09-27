using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Data.Repositories;
using Teams.Domain;
using Teams.Models;

namespace Teams.Controllers
{
    public class MultipleAnswerQuestionController : Controller
    {
        public MultipleAnswerQuestionController(ApplicationDbContext db)
        {
            _db = db;
        }
        private readonly ApplicationDbContext _db;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Guid Id)
        {
            var questionRepository = new MultipleAnswerQuestionRepository(_db);
            // test
            var a = questionRepository.PickById(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"));
            //_db.MultipleAnswerQuestions.Add(a);
            //_db.SaveChanges();
            // delete between
            return View("MultipleAnswerQuestionForm", a);
        }
        public IActionResult MultipleAnswerQuestionForm(string Id, int[] answer)
        {
            var questionRepository = new MultipleAnswerQuestionRepository(_db);
            var question = questionRepository.PickById(new Guid(Id));
            question.ChosenOptions = new List<int>(answer);
            /*bool b = true;
            for (int i = 0; i < question.answers.Count; i++)
            {
                if ((question.answers[i].isRight && !answer.Contains(i)) || (!question.answers[i].isRight && answer.Contains(i)))
                {
                    b = false;
                    break;
                }
            }*/
            return View(question);
        }
    }
}
