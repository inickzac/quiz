using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.Repositories
{
    public class ProgramCodeQuestionRepository : IProgramCodeQuestionRepository
    {
        private ApplicationDbContext _db;
        public ProgramCodeQuestionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ProgramCodeQuestion> PickByIdAsync(Guid id)
        {
            return await _db.ProgramCodeQuestions.SingleAsync(q => q.Id == id);
        }
    }
}
