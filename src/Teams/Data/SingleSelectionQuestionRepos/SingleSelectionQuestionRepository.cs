using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.SingleSelectionQuestionRepos
{
    public class SingleSelectionQuestionRepository: ISingleSelectionQuestionRepository
    {
        private IApplicationDbContext _dbContext;
        public SingleSelectionQuestionRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public SingleSelectionQuestion Get(Guid id)
        {
            return _dbContext.SingleSelectionQuestions.Include(q => q.Options)
               .FirstOrDefault(i => i.Id == id);
        }
    }
}
