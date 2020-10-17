using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            questionRepository = new MultipleAnswerQuestionRepository(_db);
        }
        private readonly ApplicationDbContext _db;
        private readonly IMultipleAnswerQuestionRepository questionRepository;
        [HttpPost]
        public IActionResult Index(Guid id)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = questionRepository.PickById(id)
            };
            return View("MultipleAnswerQuestionForm", question);
        }
        public IActionResult MultipleAnswerQuestionForm(string id, int[] answers)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = questionRepository.PickById(new Guid(id)),
                ChosenOptions = answers
            };
            return View(question);
        }
    }
}
