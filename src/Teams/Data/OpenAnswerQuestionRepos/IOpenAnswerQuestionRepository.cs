using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.OpenAnswerQuestionRepos
{
    public interface IOpenAnswerQuestionRepository
    {
        public Task<OpenAnswerQuestion> GetAsync(Guid id);
    }
}
