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
        private IApplicationDbContext _applicationDbContext;
        private ITestRepository _testContext;
        private TestRun _currentTestRun;
        private Test _currentTest;
        private TestQuestion _testQuestion;
        private ApplicationUser _currentUser;

        private List<Guid> _takenTestsIds;
        public TestRunController(ITestRunRepository testRunRepository, IAnswerRepository answerRepository, IApplicationDbContext applicationDbContext, ITestRepository testRepository, ApplicationUser user)
        {
            _testRunRepository = testRunRepository;
            _answerRepository = answerRepository;
            _applicationDbContext = applicationDbContext;
            _testContext = testRepository;
            _currentUser = user;
            _takenTestsIds = _testRunRepository.GetAllByUserId(_currentUser.Id).Select(t => t.Id).ToList();
        }
        
        public IActionResult Index()
        {
            List<Test> tests = _testContext.GetAll();
            return View(new TestRunIndexModel() {ApplicationUser = _currentUser, TakenTestsIds = _takenTestsIds, Tests = tests});
        } 

        public void Start(Guid testId)
        {
            Test currentTest = _testContext.Get(testId);
            if (!_testRunRepository.GetAllByUserId(_currentUser.Id).Any(x => x.TestId == testId))_currentTestRun = new TestRun(_currentUser, currentTest);
            _applicationDbContext.Testrun.Add(_currentTestRun);
            _applicationDbContext.SaveChanges();
            //Pass the TestRun to the controller. If the 
        }

        [HttpPost]
        public void Finish()
        {
            _currentTestRun.EndTestRun();
           // _currentTestRun = new TestRun(){TestedUserId = userId, TestId = testId}; 
        }
    }
}