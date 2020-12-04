using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public class TestRunRepository : ITestRunRepository
    {
        private IApplicationDbContext _dbContext;

        public TestRunRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TestRun> GetAll()
        {
            return _dbContext.Testrun.ToList();
        }

        public TestRun GetById(Guid id)
        {
            return _dbContext.Testrun.Find(id);
        }
    }
}
