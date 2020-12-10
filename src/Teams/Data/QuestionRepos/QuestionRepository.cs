using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teams.Domain;

namespace Teams.Data.QuestionRepos
{
    public class QuestionRepository : IQuestionRepository
    {
        private IApplicationDbContext _dbContext;
        public QuestionRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _dbContext.Questions.ToListAsync();
        }
        public async Task<List<Question>> GetTestQuestionsAsync(Guid testId)
        {
            return await _dbContext.TestQuestions.Where(w => w.TestId == testId).Select(w => w.Question).ToListAsync();
        }
    }
}
