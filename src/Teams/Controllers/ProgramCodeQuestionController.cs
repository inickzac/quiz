using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teams.Data.Repositories;
using Teams.Domain;
using Teams.Models;
using static System.IO.File;

namespace Teams.Controllers
{
    public class ProgramCodeQuestionController : Controller
    {
        private IProgramCodeQuestionRepository questionRepository;
        public ProgramCodeQuestionController(IProgramCodeQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
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
            var extensionIndex = model.File.FileName.LastIndexOf('.');
            var extesion = (extensionIndex >= 0) ? model.File.FileName.Substring(extensionIndex + 1).ToLower() : null;
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
            return Content("The file uploaded successfully!");
        }
    }
}
