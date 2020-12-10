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
        public MultipleAnswerQuestionController(IMultipleAnswerQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        private readonly IMultipleAnswerQuestionRepository questionRepository;
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = await questionRepository.PickByIdAsync(id)
            };
            return View("MultipleAnswerQuestionForm", question);
        }
        public async Task<IActionResult> MultipleAnswerQuestionForm(string id, int[] answers)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = await questionRepository.PickByIdAsync(new Guid(id)),
                ChosenOptions = answers
            };
            return View(question);
        }
    }
}
