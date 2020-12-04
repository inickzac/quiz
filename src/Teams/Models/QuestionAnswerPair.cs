using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class QuestionAnswerPair : Entity
    {
        public Guid QuestionId { get; private set; }
        public Guid AnswerId { get; private set; }
        [ForeignKey("TestRunID_FK")]
        public Guid TestRunId { get; private set; }

        public QuestionAnswerPair(Guid testRunId)
        {
            TestRunId = testRunId;
        }

        public void Add(Guid questionId, Guid answerId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
        }
    }
}
