using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teams.Data.Repositories;
using Teams.Data;
using Teams.Models;
using Teams.Domain;

namespace Teams.Controllers
{
    public class ProgramCodeQuestionController : Controller
    {
        private IProgramCodeQuestionRepository questionRepository;
        private IQueuedProgramRepository programTextRepository;
        private IApplicationDbContext _db;
        public ProgramCodeQuestionController(IApplicationDbContext db, IProgramCodeQuestionRepository questionRepository, IQueuedProgramRepository programTextRepository)
        {
            this.questionRepository = questionRepository;
            this.programTextRepository = programTextRepository;
            _db = db;
        }
        [HttpGet]
        public IActionResult Index(Guid id)
        {
            var question = questionRepository.PickById(id);
            var model = new ProgramCodeQuestionViewModel()
            {
                Id = question.Id,
                Text = question.Text
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(ProgramCodeQuestionViewModel model)
        {
            if (model.File is null)
            {
                model.AlertText = "Please upload a file before submitting.";
                return View(model);
            }
            int maxSize = (int)Math.Pow(2, 15);
            int extensionIndex = model.File.FileName.LastIndexOf('.');
            var extesion = (extensionIndex >= 0) ? model.File.FileName[(extensionIndex + 1)..].ToLower() : null;
            if (extesion != "js" && extesion != "cs")
            {
                model.AlertText = $"Wrong extension .{extesion}! Please upload only .js and .cs files";
                return View(model);
            }
            if (model.File.Length > maxSize)
            {
                model.AlertText = $"Too big file ({model.File.Length / 1024} kb). Please upload files less than 32 kb.";
                return View(model);
            }
            var question = questionRepository.PickById(model.Id);
            string programText;
            using (var sr = new StreamReader(model.File.OpenReadStream()))
            {
                programText = sr.ReadToEnd();
            }
            programTextRepository.Add(question.Id, programText);
            _db.SaveChanges();
            return Content($"The file uploaded successfully!");
        }
    }
}
