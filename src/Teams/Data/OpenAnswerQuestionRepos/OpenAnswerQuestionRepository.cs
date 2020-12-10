using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<OpenAnswerQuestion> GetAsync(Guid id)
        {
            return await context.OpenAnswerQuestions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
