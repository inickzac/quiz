using System;
using System.Collections.Generic;
using System.Linq;

namespace Teams.Domain
{
    /// <summary>
    ///     Class Answer contains answers to current Test question. Answer is stored as List strings.
    /// </summary>
    public class Answer : Entity
    {
        public AnswerValue AnswerValue { get; private set; }
        public Guid TestQuestionId { get; }
        
        public Answer()
        {
        }

        public Answer(AnswerValue answerValue, Guid testQuestionId)
        {
            AnswerValue = answerValue;
            TestQuestionId = testQuestionId;
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
            AnswerValue.AddAnswer(answerIds.Select(x => x.ToString()).ToList(), true);
        }
    }
}