using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AnswerRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Answer>> GetAllAsync()
        {
            return await _applicationDbContext.Answers.ToListAsync();
        }

        public async Task<Answer> GetByIdAsync(Guid id)
        {
            List<Answer> answers = await GetAllAsync();
            return answers.FirstOrDefault(a => a.Id == id);
        }
    }
}