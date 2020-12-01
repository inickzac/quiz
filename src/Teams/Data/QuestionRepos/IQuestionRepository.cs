using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.QuestionRepos
{
    public interface IQuestionRepository
    {
        public List<Question> GetQuestions();
        public List<Question> GetTestQuestions(Guid testId);
    }
}
