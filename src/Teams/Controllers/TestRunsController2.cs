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
    public class TestRunsController2 : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUser _applicationUser;
        private IAnswerRepository _answerRepository;
        private ITestRunRepository _testRunRepository;

        public TestRunsController2(ApplicationDbContext context, ApplicationUser applicationUser, AnswerRepository answerRepository, TestRunRepository testRunRepository)
        {
            _context = context;
            _applicationUser = applicationUser;
            _answerRepository = answerRepository;
            _testRunRepository = testRunRepository;
        }

        // GET: TestRunsController2
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
        
        [HttpGet]
        public async Task<IActionResult> AddAnswer(Guid testRunId, Answer answer)
        {
            var testRun = await _context.Testrun.FindAsync(testRunId);
            if (testRun == null)
            {
                return NotFound();
            }
            
            ViewData["TestedUserID"] = new SelectList(_context.Users, "Id", "Id", testRun.TestedUserID);
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
            _testRunRepository.GetByIdAsync(testRunId).Result.AnswersCounter++;
            return View(testRun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TestedUserID,TestId,InProgress,AnswersCounter,Id")] TestRun testRun)
        {
            if (id != testRun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testRun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestRunExists(testRun.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestedUserID"] = new SelectList(_context.Users, "Id", "Id", testRun.TestedUserID);
            return View(testRun);
        }

        // GET: TestRunsController2/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testRun = await _context.Testrun
                .Include(t => t.TestedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testRun == null)
            {
                return NotFound();
            }

            return View(testRun);
        }

        // POST: TestRunsController2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var testRun = await _context.Testrun.FindAsync(id);
            _context.Testrun.Remove(testRun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestRunExists(Guid id)
        {
            return _context.Testrun.Any(e => e.Id == id);
        }
    }
}
