using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.Repositories
{
    public class MultipleAnswerQuestionRepository : IMultipleAnswerQuestionRepository
    {
        ApplicationDbContext _db;
        public MultipleAnswerQuestionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<MultipleAnswerQuestion> PickByIdAsync(Guid Id)
        {
            return await _db.MultipleAnswerQuestions.Include(a => a.Answers).SingleAsync(q => q.Id == Id);
        }
    }
}
