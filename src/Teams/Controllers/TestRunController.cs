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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IAnswerRepository _answerRepository;

        public TestRunController(ITestRunRepository testRunRepository, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _testRunRepository = testRunRepository;
            _applicationDbContext = applicationDbContext;
            _signInManager = signManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(FormCollection inputText, Test test)
        {
            string answerText = inputText["answer_text"];
            List<string> answerList;
            _applicationUser = await _userManager.GetUserAsync(User);
            TestRun testRun = await _testRunRepository.GetByTestIdAsync(test.Id) ?? new TestRun(_applicationUser, test);
            if (testRun.InProgress)
            {
                List<TestQuestion> testQuestions = testRun.Test.TestQuestions.ToList();
                int answersCount = 1;

                if (testRun.InProgress)
                {
                    foreach (var question in testQuestions)
                    {
                        if (question.Question is MultipleAnswerQuestion tempQuestion)
                        {
                            answersCount = tempQuestion.Answers.Count;
                        }
                    }
                }
                return View(testRun, answersCount);
            }

            RedirectToAction("ShowUserTestRuns", _applicationUser.Id);
        }
        
        //[HttpGet]
        //public IActionResult Start(Guid testRunId, string userId)
        //{
        //    if (_testRunRepository.GetAllAsync().Result.All(x => x.Id != testRunId))
        //    {
        //        return NotFound();
        //    }
        //    TestRun testRun = _testRunRepository.GetAllAsync().Result.FirstOrDefault(x => x.TestId == testRunId);
        //    return View("Run", testRun);
        //}

        [HttpPost]
        private async Task<bool> Create(Guid testId)
        {
            Test test = await _applicationDbContext.Tests.FindAsync(testId);
            TestRun testRun = new TestRun(_applicationUser, test);
            await SaveTestRun(testRun);
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(Answer answer, Guid testRunTd)
        {
            await _applicationDbContext.Answers.AddAsync(answer);
            await _applicationDbContext.SaveChangesAsync();
            _testRunRepository.GetByIdAsync(testRunTd).Result.AnswersCounter++;
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Finish(TestRun testRun)
        {
            testRun.InProgress = false;
            await SaveTestRun(testRun);
            return RedirectToAction("Index");
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
                await _applicationDbContext.Testrun.AddAsync(testRun);
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