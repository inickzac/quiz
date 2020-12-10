using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teams.Domain;

namespace Teams.Data.TestRepos
{
    public interface ITestRepository 
    {
        public Task<List<Test>> GetAllAsync();
        public Task<Test> GetAsync(Guid id);
    }
}
