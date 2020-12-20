using System;
using System.Collections.Generic;
using Teams.Domain;

namespace Teams.Models
{
    public class TestRunDTO
    {
        public Guid Id { get; set; }
        public List<AnswerDTO> Answers { get; set; }
        public List<TestQuestion> TestQuestions { get; set; }
        public Guid TestId { get; set; }

        public TestRunDTO(List<AnswerDTO> answers, List<TestQuestion> questions, Guid testId)
        {
            Id = Guid.NewGuid();
            Answers = answers;
            TestQuestions = questions;
            TestId = testId;
        }
    }
}