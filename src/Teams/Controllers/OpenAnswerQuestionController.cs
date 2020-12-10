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

        public async Task<IActionResult> Question(Guid id)
        {

            var question = await context.GetAsync(id);                                
            
            if(question == null) return NotFound();

            OpenAnswerQuestionModel ivm = new OpenAnswerQuestionModel
            { 
                Id = id,
                Question = question.Text
            };

            return View(ivm);
        }


        public async Task<IActionResult> Answer(string answer, Guid id)
        {
            var question = await context.GetAsync(id);
           
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