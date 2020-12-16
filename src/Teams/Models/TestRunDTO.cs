using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Teams.Domain.DTO_Models;

namespace Teams.Models
{
    public class TestRunDTO
    {
        public Guid Id { get; set;}
        public List<AnswerDTO> Answers { get; set; }
        public List<TestQuestion> TestQuestions { get; set; }

        public TestRunDTO(List<AnswerDTO> answers)
        {
            Id = Guid.NewGuid();
            Answers = answers;
        }

        public TestRunDTO(List<AnswerDTO> answers, List<TestQuestion> questions)
        {
            Id = Guid.NewGuid();
            Answers = answers;
            TestQuestions = questions;
        }
    }
}
