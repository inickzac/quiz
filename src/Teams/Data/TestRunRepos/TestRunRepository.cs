using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Models;
using Microsoft.EntityFrameworkCore;
using Teams.Domain;

namespace Teams.Data.TestRunRepos
{
    public class TestRunRepository : ITestRunRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public TestRunRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TestRun>> GetAllAsync() => await _dbContext.TestRuns.ToListAsync();

        public async Task<TestRun> GetByIdAsync(Guid id)
        {
            return await _dbContext.TestRuns.FindAsync(id);
        }

        public async Task<List<TestRun>> GetAllByUserAsync(string id)
        {
            List<TestRun> testRuns = await GetAllAsync();
            return testRuns.Where(x => x.TestedUserId == id).ToList();
        }
    }
}
