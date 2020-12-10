using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Domain;

namespace Teams.Data.TestRepos
{
    public class TestRepository : ITestRepository
    {
        private IApplicationDbContext _dbContext;
        public TestRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Test>> GetAllAsync() => await _dbContext.Tests.ToListAsync();
        public async Task<Test> GetAsync(Guid id) => await _dbContext.Tests.FindAsync(id);
    }
}
