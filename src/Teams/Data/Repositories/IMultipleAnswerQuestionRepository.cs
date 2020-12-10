using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.Repositories
{
    public interface IMultipleAnswerQuestionRepository
    {
        Task<MultipleAnswerQuestion> PickByIdAsync(Guid Id);
    }
}
