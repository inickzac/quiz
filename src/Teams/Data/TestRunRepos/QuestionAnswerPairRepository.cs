using System;
using System.Collections.Generic;
using System.Linq;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public class QuestionAnswerPairRepository : IQuestionAnswerPairRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public QuestionAnswerPairRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<QuestionAnswer> GetAll()
        {
            return _dbContext.QuestionAnswerPairs.ToList();
        }

        public QuestionAnswer GetById(Guid id)
        {
            return _dbContext.QuestionAnswerPairs.Find(id);
        }
    }
}