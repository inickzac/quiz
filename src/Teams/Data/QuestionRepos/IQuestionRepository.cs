using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.QuestionRepos
{
    public interface IQuestionRepository
    {
        public Task<List<Question>> GetQuestionsAsync();
        public Task<List<Question>> GetTestQuestionsAsync(Guid testId);
    }
}
