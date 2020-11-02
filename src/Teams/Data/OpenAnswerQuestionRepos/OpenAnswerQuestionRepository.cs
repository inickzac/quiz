using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.OpenAnswerQuestionRepos
{
    public class OpenAnswerQuestionRepository : IOpenAnswerQuestionRepository
    {
        IApplicationDbContext context;
        public OpenAnswerQuestionRepository(IApplicationDbContext context)
        {
            this.context = context;
        }
        public OpenAnswerQuestion Get(Guid id)
        {
            return context.OpenAnswerQuestions.FirstOrDefault(x => x.Id == id);
        }
    }
}
