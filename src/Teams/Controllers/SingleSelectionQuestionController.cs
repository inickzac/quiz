using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teams.Domain;
using Teams.Data;
using Microsoft.EntityFrameworkCore;
using Teams.Data.SingleSelectionQuestionRepos;

namespace Teams.Controllers
{
    public class SingleSelectionQuestionController : Controller
    {
        private ISingleSelectionQuestionRepository _singleRepository { get; set; }
        public SingleSelectionQuestionController(ISingleSelectionQuestionRepository singleRepository)
        {
            _singleRepository = singleRepository;
        }
        [Route("[Controller]/{id?}")]
        public IActionResult Index(Guid id)
        {
            var question = _singleRepository.Get(id);
            if (question == null) return NotFound();
            return View(question);
        }
        [HttpGet]
        public JsonResult FindAnswer(Guid questionId)
        {
            var question = _singleRepository.Get(questionId);
            var answer = question.GetRightAnswer();
            return Json(answer);
        }
    }
}
