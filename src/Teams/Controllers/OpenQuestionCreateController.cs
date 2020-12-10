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
            OpenAnswerQuestionAsync questionAsync = new OpenAnswerQuestionAsync(modelForView.Question, modelForView.Answer);
            _db.OpenAnswerQuestions.Add(questionAsync);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            OpenAnswerQuestionAsync questionAsync = await _db.OpenAnswerQuestions.FirstOrDefaultAsync(p => p.Id == id);
            if (questionAsync != null)
            {
                OpenAnswerQuestionModel modelForView = new OpenAnswerQuestionModel
                { 
                    Question = questionAsync.Text,
                    Answer = questionAsync.Answer,
                    Id = questionAsync.Id 
                };

                return View(modelForView);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OpenAnswerQuestionModel modelForView)
        {
            OpenAnswerQuestionAsync questionAsync = await _db.OpenAnswerQuestions.FirstOrDefaultAsync(p => p.Id == modelForView.Id);
            questionAsync.UpdateQuestion(modelForView.Question, modelForView.Answer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}