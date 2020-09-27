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
        public MultipleAnswerQuestion PickById(Guid Id)
        {
            var q = from question in _db.MultipleAnswerQuestions
                    where question.Id == Id
                    select question;
            return q.First();
        }
    }
}
