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
        public List<Test> GetAll();
        public Test Get(Guid id);
    }
}
