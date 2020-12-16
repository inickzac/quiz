using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Teams.Data;
using Teams.Domain;
using Teams.Models;

namespace Teams.Controllers
{
    public class OpenQuestionCreateController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OpenQuestionCreateController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OpenAnswerQuestionModel modelForView)
        {          
            OpenAnswerQuestion question = new OpenAnswerQuestion(modelForView.Question, modelForView.Answer);
            _db.OpenAnswerQuestions.Add(question);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            OpenAnswerQuestion question = await _db.OpenAnswerQuestions.FirstOrDefaultAsync(p => p.Id == id);
            if (question != null)
            {
                OpenAnswerQuestionModel modelForView = new OpenAnswerQuestionModel
                { 
                    Question = question.Text,
                    Answer = question.Answer,
                    Id = question.Id 
                };

                return View(modelForView);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OpenAnswerQuestionModel modelForView)
        {
            OpenAnswerQuestion question = await _db.OpenAnswerQuestions.FirstOrDefaultAsync(p => p.Id == modelForView.Id);
            question.UpdateQuestion(modelForView.Question, modelForView.Answer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}