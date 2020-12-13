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

        public TestRunController(ITestRunRepository testRunRepository, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _testRunRepository = testRunRepository;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ViewResult> Index(Guid testId)
        {
            _applicationUser = await _userManager.GetUserAsync(User);
            TestRun testRun = await _testRunRepository.GetByTestIdAsync(testId) ?? new TestRun(_applicationUser, _applicationDbContext.Tests.FirstOrDefault(x=> x.Id == testId));
            foreach (var question in testRun.Test.TestQuestions)
            {
                AnswerViewModel answerViewModel = new AnswerViewModel() {TestQuestion =  question, TestRunGuid = testRun.Id};
                View("AddAnswer", answerViewModel);
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void AddAnswer(AnswerViewModel answerViewModel)
        {
            if (answerViewModel == null)
            {
                throw new Exception();
            }

            var currentTestRun = await _testRunRepository.GetByIdAsync(answerViewModel.TestRunGuid);
            Answer  currentAnswer = new Answer();
            currentAnswer.SetAnswer(answerViewModel.AnswerTexts);
            currentAnswer.TestRun = await _testRunRepository.GetByIdAsync(currentTestRun.Id);
            currentAnswer.TestRunFK = currentAnswer.Id;
            await _applicationDbContext.Answers.AddAsync(currentAnswer);
            _applicationDbContext.Testrun.Update(currentTestRun);
            await _applicationDbContext.SaveChangesAsync();
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