using System;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Data.TestRepos;
using Teams.Data.TestRunRepos;
using Teams.Domain;
using Teams.Models;
using System.Collections.Generic;
using System.Linq;

namespace Teams.Controllers
{
    public class TestRunController : Controller
    {
        private ITestRunRepository _testRunRepository;
        private IAnswerRepository _answerRepository;
        private IQuestionAnswerPairRepository _questionAnswerPairRepository;
        private IApplicationDbContext _applicationDbContext;
        private ITestRepository _testRepository;
        private TestRun _currentTestRun;
        private Test _currentTest;
        private TestQuestion _testQuestion;
        private ApplicationUser _currentUser;

        public TestRunController(ITestRunRepository testRunRepository, IAnswerRepository answerRepository, IQuestionAnswerPairRepository questionAnswerPairRepository, IApplicationDbContext applicationDbContext, ITestRepository testRepository, ApplicationUser user)
        {
            _testRunRepository = testRunRepository;
            _answerRepository = answerRepository;
            _questionAnswerPairRepository = questionAnswerPairRepository;
            _applicationDbContext = applicationDbContext;
            _testRepository = testRepository;
            _currentUser = user;
        }
        
        public IActionResult Index()
        {
            List<Test> tests = _testRepository.GetAll();
            List<Guid> takenTestsIds = _testRunRepository.GetAllByUserId(_currentUser.Id).Select(t => t.Id).ToList();
            return View(new TestRunViewModel() {ApplicationUser = _currentUser, TakenTestsIds = takenTestsIds, Tests = tests});
        } 

        public void StartTestRun(string userId, Guid testId)
        {
            _currentTestRun = _testRunRepository.GetAllByUserId(userId).Find(x => x.TestId == testId);
            if (_currentTestRun == null)
                Create(userId, testId);
            var currentQuestions = _testRepository.Get(testId).TestQuestions;
            foreach (TestQuestion question in currentQuestions)
            {
                
            }
            
            var currentUserTestRuns = _testRunRepository.GetAllByUserId(userId);
        }

        [HttpPost]
        public void Create([FromBody] string userId, [FromBody] Guid testId)
        {
           // _currentTestRun = new TestRun(){TestedUserId = userId, TestId = testId}; 
        }
    }
}