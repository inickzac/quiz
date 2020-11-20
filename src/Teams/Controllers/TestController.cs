using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Data;
using Teams.Data.QuestionRepos;
using Teams.Data.TestRepos;
using Teams.Domain;
using Teams.Domain.DTO_Models;

namespace Teams.Controllers
{
    public class TestController : Controller
    {
        private ITestRepository _testRepository;
        private IQuestionRepository _questionRepository;
        private IApplicationDbContext _dbContext;
        public TestController(ITestRepository testRepository, IQuestionRepository questionRepository, IApplicationDbContext dbContext)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_testRepository.GetAll());
        }
        [HttpPost]
        public IActionResult Create([FromBody] string name)
        {
            var newTest = new Test(name);
            _dbContext.Tests.Add(newTest);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var test = _testRepository.Get(id);
            if (test == null)
            {
                return NotFound();
            }
            var testDto = new TestDTO() 
            {
                Id = test.Id,
                Title = test.Title
            };
            testDto.Questions = _questionRepository
                .GetTestQuestions(id)
                .Select(item => new QuestionDTO(item.Id, item.Text))
                .ToList();
            return View(testDto);
        }
        [HttpPost]
        public IActionResult Edit(TestDTO test)
        {
            if (test == null)
            {
                return BadRequest();
            }
            var testQuestions = _dbContext.TestQuestions.Where(w => w.TestId == test.Id);
            _dbContext.TestQuestions.RemoveRange(testQuestions);
            foreach (var item in test.Questions)
            {
                _dbContext.TestQuestions.Add(new TestQuestion(test.Id, item.Id));
            }
            var updatedTest = new Test(test.Id, test.Title,
                _dbContext.TestQuestions.Where(w => w.TestId == test.Id).ToList());
            _dbContext.Tests.Update(updatedTest);
            _dbContext.SaveChanges();

            return View(test);
        }
        [HttpPost]
        public IActionResult DeleteQuestion([FromBody] TestDTO test, Guid id)
        {
            test.Questions.RemoveAll(w => w.Id == id);
            return PartialView("_EditPartial", test);
        }
        [HttpPost]
        public IActionResult AddQuestion(List<Guid> id, [FromBody] TestDTO test)
        {
            if (test == null)
            {
                return BadRequest();
            }
            foreach (var item in id)
            {
                var question = _dbContext.Questions.FirstOrDefault(w => w.Id == item);
                if (question == null)
                {
                    return BadRequest();
                }
                var questionDTO = new QuestionDTO(question.Id, question.Text);
                if (!test.Questions.Any(w => w.Id == item))
                {
                    test.Questions.Add(questionDTO);
                }
            }
            return PartialView("_EditPartial", test);
        }
        public IActionResult Delete(Guid id)
        {
            var test = _testRepository.Get(id);
            if (test != null)
            {
                _dbContext.Tests.Remove(test);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
