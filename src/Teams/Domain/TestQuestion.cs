using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public class TestQuestion: Entity
    {
        public Guid TestId { get; private set; }
        public Test Test { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; }
        public TestQuestion(Guid testId, Guid questionId)
        {
            TestId = testId;
            QuestionId = questionId;
        }
    }
}
