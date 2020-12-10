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

        public IActionResult EditMultipleAnswerQuestion(Guid id)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = questionRepository.PickById(id),
                // ChosenOptions = answers
            };
            return View(question);
        }

        public IActionResult AddMultipleAnswerQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMultipleAnswerQuestion([FromBody] MultipleAnswerQuestionDTOModel fromAjax)
        {
            List<MultipleAnswerQuestionOption> allAnswers = new List<MultipleAnswerQuestionOption>();
            for (int i = 0; i < fromAjax.TextMassive.Length; i++)
            {
                allAnswers.Add(new MultipleAnswerQuestionOption(fromAjax.TextMassive[i], fromAjax.CheckboxValueMassive[i]));
            }
            questionRepository.AddQuestion(new MultipleAnswerQuestion(fromAjax.questionText, allAnswers));

            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public IActionResult EditMultipleAnswerQuestion([FromBody] MultipleQuestionEditModel fromAjax)
        {
            List<MultipleAnswerQuestionOption> allAnswers = new List<MultipleAnswerQuestionOption>();
            for (int i = 0; i < fromAjax.TextMassive.Length; i++)
            {
                allAnswers.Add(new MultipleAnswerQuestionOption(fromAjax.TextMassive[i], fromAjax.CheckboxValueMassive[i]));
            }
            MultipleAnswerQuestion question = questionRepository.PickById(fromAjax.id);
            question.UpdateQuestion(fromAjax.questionText, allAnswers);
            questionRepository.SaveAllChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
