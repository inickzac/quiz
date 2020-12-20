using System;
using System.Collections.Generic;
using System.Linq;

namespace Teams.Domain
{
    public class Answer 
    {
        public Guid Id { get; }
        public IReadOnlyCollection<string> AnswerOptions => _answerOptions.AsReadOnly();
        private readonly List<string> _answerOptions;
        private readonly string _AnswerText;
        public string AnswerText => _AnswerText;
        public Guid TestQuestionId { get; }
        
        public Answer()
        {
        }

        public Answer(string answerText, List<Guid> answerIds, Guid testQuestionId, Guid id)
        {
            foreach (var i in answerIds) _answerOptions.Add(i.ToString());
            Id = id;
            _AnswerText = answerText;
            TestQuestionId = testQuestionId;
        }
    }
}