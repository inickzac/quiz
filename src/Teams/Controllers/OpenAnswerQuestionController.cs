using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Data.OpenAnswerQuestionRepos;
using Teams.Models;

namespace Teams.Controllers
{
    public class OpenAnswerQuestionController : Controller
    {

        public IOpenAnswerQuestionRepository context;
       
        public OpenAnswerQuestionController(IOpenAnswerQuestionRepository context)
        {
            this.context = context;
        }

        public IActionResult Question(Guid id)
        {

            var question = context.Get(id);                                
            
            if(question == null) return NotFound();

            OpenAnswerQuestionModel ivm = new OpenAnswerQuestionModel
            { 
                Id = id,
                Question = question.Text
            };

            return View(ivm);
        }


        public IActionResult Answer(string answer, Guid id)
        {
            var question = context.Get(id);
           
            OpenAnswerQuestionModel ivm = new OpenAnswerQuestionModel
            {
                Id = id,
                Question = question.Text,
                Answer = question.Answer,
                IsAnswer = question.IsCorrectAnswer(answer)                
            };

            return View(ivm);
        }

    }
}