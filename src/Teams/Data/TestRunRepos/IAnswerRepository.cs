using System;
using System.Collections.Generic;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public interface IAnswerRepository
    {
        public List<Answer> GetAll();
        public TestRun GetById(Guid id);
    }
}