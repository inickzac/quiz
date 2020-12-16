using System;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Data.TestRunRepos;
using Teams.Domain;
using Teams.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Teams.Controllers
{
    public class TestRunController : Controller
    {
        private ITestRunRepository _testRunRepository;
        private ApplicationDbContext _applicationDbContext;
        private IAnswerRepository _answerRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TestRunController(ITestRunRepository testRunRepository, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
            _testRunRepository = testRunRepository;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid testRunId)
        {
            var testRun = await _testRunRepository.GetByIdAsync(testRunId);
            if (testRun == null) return NotFound();
            var testQuestions =
                _applicationDbContext.TestQuestions.Where(q => q.TestId == testRun.TestId).ToList();
            Start(testRun);
            var testRunDto = new TestRunDTO(testRun.Answers.Select(a => new AnswerDTO(a.AnswerValue, 
                a.Id, testRunId, a.TestQuestionId)).ToList(),  testQuestions);
            return View(testRunDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(TestRunDTO testRunDto)
        {
            var updatedTestRun = await _testRunRepository.GetByIdAsync(testRunDto.Id);
            if (updatedTestRun == null) return NotFound();
            foreach (var answer in testRunDto.Answers)
            {
                updatedTestRun.Answers.Add(await _answerRepository.GetByIdAsync(answer.Id));
            }

            Finish(updatedTestRun);
            return View(testRunDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnswer(AnswerDTO answerDto)
        {
            if (answerDto == null) return NotFound();
            var answer = new Answer(answerDto.AnswerValue, answerDto.TestQuestionId);
            var  currentAnswer = await _answerRepository.GetByIdAsync(answerDto.Id);
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