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
        public IActionResult AddMultipleAnswerQuestion([FromBody] MAQDTOModel fromAjax)
        {
            List<MultipleAnswerQuestionOption> allAnswers = new List<MultipleAnswerQuestionOption>();
            for (int i = 0; i < fromAjax.jTextList.Length; i++)
            {
                allAnswers.Add(new MultipleAnswerQuestionOption(fromAjax.jTextList[i], fromAjax.jCheckboxList[i]));
            }
            questionRepository.MethodForAdd(new MultipleAnswerQuestion(fromAjax.questionText, allAnswers));

            return View(); 
            
        }
        [HttpPost]
        public IActionResult EditMultipleAnswerQuestion( int[] editAnswers, string[] answersText, string questionText, Guid Id)
        {
            List<MultipleAnswerQuestionOption> allAnswers = new List<MultipleAnswerQuestionOption>();
            int g = 0;
            bool isRight;
            for (int i = 0; i < answersText.Length; i++)
            {
                if (editAnswers[g] == i)
                {
                    isRight = true;
                    g++;
                }
                else
                {
                    isRight = false;
                }
                allAnswers.Add(new MultipleAnswerQuestionOption(answersText[i], isRight));
            }
            MultipleAnswerQuestion question = questionRepository.PickById(Id);
            question.UpdateQuestion(questionText, allAnswers);
            questionRepository.SaveAllChanges();
            return View();
        }

    }
}
