using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public interface IAnswerRepository
    {
        public Task<List<Answer>> GetAllAsync();
        public Task<Answer> GetByIdAsync(Guid id);
    }
}