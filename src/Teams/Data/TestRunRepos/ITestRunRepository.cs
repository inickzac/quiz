using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public interface ITestRunRepository
    {
        public Task<List<TestRun>> GetAllAsync();
        public Task<TestRun> GetByIdAsync(Guid id);
        public Task<TestRun> GetByTestIdAsync(Guid id);
        public Task<List<TestRun>> GetAllByUserAsync(string id);
    }
}
