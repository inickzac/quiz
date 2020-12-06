using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public interface ITestRunRepository
    {
        public List<TestRun> GetAll();
        public TestRun GetById(Guid id);
        public List<TestRun> GetAllByUserId(string id);
    }
}
