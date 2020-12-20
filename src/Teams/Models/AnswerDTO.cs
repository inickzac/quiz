using System;
using System.Collections.Generic;

namespace Teams.Models
{
    public class AnswerDTO
    {
        public Guid Id { get; set; }
        public ICollection<Guid> AnswerOptions { get; set; }
        public string AnswerText { get; set; }
        public Guid TestRunId { get; set; }
        public Guid TestQuestionId { get; set; }

        public AnswerDTO()
        {
            Id = Guid.NewGuid();
        }

        public AnswerDTO(List<string> answers, string answer, Guid id, Guid testRunId, Guid testQuestionId)
        {
            AnswerText = answer;
            Id = id;
            foreach (var a in answers) AnswerOptions.Add(new Guid(a));
            TestRunId = testRunId;
            TestQuestionId = testQuestionId;
        }
    }
}