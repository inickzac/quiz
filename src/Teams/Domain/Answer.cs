using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Teams.Domain
{
    /// <summary>
    /// Class Answer contains answers to current Test question. Answer is stored as List strings.
    /// </summary>
    public class Answer : Entity
    {
        public AnswerValue AnswerValue { get; private set; }
        public Guid TestQuestionId { get; private set; }
        public Guid TestRunId { get; private set; }

        public Answer()
        {
        }

        public Answer(AnswerValue answerValue, Guid testQuestionId, Guid testRunId)
        {
            AnswerValue = answerValue;
            TestQuestionId = testQuestionId;
            TestRunId = testRunId;
        }

        public void SetAnswer(AnswerValue answerValue)
        {
            AnswerValue = answerValue;
        }

        public void SetAnswer(string answer)
        {
            AnswerValue.AddAnswer(answer, false);
        }

        public void SetAnswer(List<string> answers)
        {
            AnswerValue.AddAnswer(answers, false);
        }
        
        public void SetAnswer(Guid answerId)
        {
            AnswerValue.AddAnswer(answerId.ToString(), true);
        }

        public void SetAnswer(List<Guid> answerIds)
        {
            AnswerValue.AddAnswer(answerIds.Select(x=> x.ToString()).ToList(), true);
        }
    }
}
