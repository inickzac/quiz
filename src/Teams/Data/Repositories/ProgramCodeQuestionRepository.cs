using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data.Repositories
{
    public class ProgramCodeQuestionRepository : IProgramCodeQuestionRepository
    {
        private ApplicationDbContext _db;
        public ProgramCodeQuestionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public ProgramCodeQuestion PickById(Guid id)
        {
            return _db.ProgramCodeQuestions.Single(q => q.Id == id);
        }
        public void Add(ProgramCodeQuestion question)
        {
            _db.ProgramCodeQuestions.Add(question);
        }
    }
}
