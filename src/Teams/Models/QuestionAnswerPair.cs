using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class QuestionAnswerPair : Entity
    {
        public ICollection<Guid> QuestionId { get; private set; }
        public ICollection<Guid> AnswerId { get; private set; }

        public void Add(Guid questionId, Guid answerId)
        {
            QuestionId.Add(questionId);
            AnswerId.Add(answerId);
        }
    }
}
