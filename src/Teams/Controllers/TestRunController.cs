using System;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Data.TestRepos;
using Teams.Data.TestRunRepos;
using Teams.Domain;
using Teams.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Teams.Domain.DTO_Models;

namespace Teams.Controllers
{
    public class TestRunController : Controller
    {
        private ITestRunRepository _testRunRepository;
        private ApplicationDbContext _applicationDbContext;
        private ApplicationUser _applicationUser;
        private AnswerRepository _answerRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TestRunController(ITestRunRepository testRunRepository, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, AnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
            _testRunRepository = testRunRepository;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid TestRunId)
        {
            _applicationUser = await _userManager.GetUserAsync(User);
            TestRun testRun = await _testRunRepository.GetByIdAsync(TestRunId);
            if (testRun == null) return NotFound();
            Start(testRun);
            TestRunDTO testRunDto = new TestRunDTO(testRun.Answers.Select(a => new AnswerDTO(a.AnswerValue, 
                a.Id, a.TestQuestionId)).ToList(),  _applicationDbContext.TestQuestions.
                Where(t=> testRun.TestQuestionIds.Contains(t.QuestionId)).ToList());
            return View(testRunDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(TestRunDTO testRunDto)
        {
            var updatedTestRun = await _testRunRepository.GetByIdAsync(testRunDto.Id);
            if (updatedTestRun == null) return NotFound();
            foreach (var answer in testRunDto.Answers)
            {
                updatedTestRun.AnswerIds.Add(answer.Id);
            }

            await SaveTestRun(updatedTestRun);
            return View(testRunDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AnswerDTO answerDto)
        {
            if (answerDto == null) return NotFound();
            Answer  currentAnswer = await _answerRepository.GetByIdAsync(answerDto.Id);
            if (currentAnswer == null) return NotFound();
            currentAnswer.SetAnswer(answerDto.AnswerValue);
            _applicationDbContext.Answers.Update(currentAnswer);
            await _applicationDbContext.SaveChangesAsync();
            return View();
        }

        public async void Start(TestRun testRun)
        {
            testRun.InProgress = true;
            await SaveTestRun(testRun);
        }

        public async void Finish(TestRun testRun)
        {
            testRun.InProgress = false;
            await SaveTestRun(testRun);
        }

        [Authorize]
        public async Task<IActionResult> ShowUserTestRuns(string userId)
        {
            var testRuns = await _testRunRepository.GetAllByUserAsync(userId);
            return RedirectToAction("Index");
        }

        private async Task<bool> SaveTestRun(TestRun testRun)
        {
            try
            {
                _applicationDbContext.Testrun.Update(testRun);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}