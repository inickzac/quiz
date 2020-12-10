using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teams.Data;
using Teams.Data.TestRunRepos;
using Teams.Domain;
using Teams.Models;

namespace Teams.Controllers
{
    public class TestRunsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationUser _applicationUser;
        private IAnswerRepository _answerRepository;
        private ITestRunRepository _testRunRepository;

        public TestRunsController(ApplicationDbContext context, ApplicationUser applicationUser, AnswerRepository answerRepository, TestRunRepository testRunRepository)
        {
            _context = context;
            _applicationUser = applicationUser;
            _answerRepository = answerRepository;
            _testRunRepository = testRunRepository;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Testrun.Include(t => t.TestedUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TestRunsController2/Create
        public IActionResult Create()
        {
            ViewData["TestedUserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("TestedUserID,TestId,InProgress,AnswersCounter,Id")] TestRun testRun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testRun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestedUserID"] = new SelectList(_context.Users, "Id", "Id", testRun.TestedUserID);
            return View(testRun);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAnswer(Guid testRunId, Answer answer)
        {
            var testRun = await _context.Testrun.FindAsync(testRunId);
            if (testRun == null)
            {
                return NotFound();
            }
            await _context.Answers.AddAsync(answer);
            testRun.AnswersCounter = 0;
            await SaveTestRun(testRun);
            _testRunRepository.GetByIdAsync(testRunId).Result.AnswersCounter++;
            return View(testRun);
        }
        
        [HttpPost]
        public async Task<IActionResult> Finish(TestRun testRun)
        {
            testRun.InProgress = false;
            await SaveTestRun(testRun);
            return RedirectToAction("Index");
        }

        private bool TestRunExists(Guid id)
        {
            return _context.Testrun.Any(e => e.Id == id);
        }
        
        private async Task<bool> SaveTestRun(TestRun testRun)
        {
            try
            {
                await _context.Testrun.AddAsync(testRun);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public async Task<IActionResult> ShowUserTestRuns(string userId)
        {
            var testRuns = await _testRunRepository.GetAllByUserAsync(userId);
            return View(testRuns);
        }
    }
}
