using System;
using System.Collections.Generic;
using System.Linq;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AnswerRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<Answer> GetAll()
        {
            return _applicationDbContext.Answers.ToList();
        }

        public TestRun GetById(Guid id)
        {
            return _applicationDbContext.Testrun.Find(id);
        }
    }
}