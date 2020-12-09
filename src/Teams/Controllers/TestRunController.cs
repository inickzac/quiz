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
using Teams.Domain.DTO_Models;

namespace Teams.Controllers
{
    public class TestRunController : Controller
    {
        private ITestRunRepository _testRunRepository;
        private ApplicationDbContext _applicationDbContext;
        private readonly ApplicationUser _applicationUser;

        public TestRunController(ITestRunRepository testRunRepository, ApplicationDbContext applicationDbContext, ApplicationUser applicationUser)
        {
            _testRunRepository = testRunRepository;
            _applicationDbContext = applicationDbContext;
            _applicationUser = applicationUser;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TestRun> testRuns = await  _testRunRepository.GetAllByUserAsync(_applicationUser.Id); 
            return View(testRuns);
        }
        [HttpGet]
        public async Task<IActionResult> Index(string userId)
        {
            List<TestRun> testRuns = await  _testRunRepository.GetAllByUserAsync(userId); 
            return View(testRuns);
        }
        [HttpGet]
        public IActionResult Start(Guid testRunId, string userId)
        {
            if (_testRunRepository.GetAllAsync().Result.All(x => x.Id != testRunId))
            {
                return NotFound();
            }
            TestRun testRun = _testRunRepository.GetAllAsync().Result.FirstOrDefault(x => x.TestId == testRunId);
            return View("Run", testRun);
        }

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
            return RedirectToAction("Start");
        }
        
        [HttpPost]
        public async Task<IActionResult> Finish(TestRun testRun)
        {
            testRun.InProgress = false;
            await SaveTestRun(testRun);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> GetUserTestRuns(string userId)
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