using System;
using System.Collections.Generic;
using Teams.Models;

namespace Teams.Data.TestRunRepos
{
    public interface IQuestionAnswerPairRepository
    {
        public List<QuestionAnswer> GetAll();
        public QuestionAnswer GetById(Guid id);
    }
}