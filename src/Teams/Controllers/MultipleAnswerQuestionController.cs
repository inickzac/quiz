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
            var question = questionRepository.PickById(Id);
            return View("MultipleAnswerQuestionForm", question);
        }
        public IActionResult MultipleAnswerQuestionForm(string Id, int[] answer)
        {
            var questionRepository = new MultipleAnswerQuestionRepository(_db);
            var question = questionRepository.PickById(new Guid(Id));
            question.ChosenOptions = new List<int>(answer);
            return View(question);
        }
    }
}
