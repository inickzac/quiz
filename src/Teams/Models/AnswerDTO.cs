using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class AnswerDTO 
    {
        public Guid Id { get; set; }
        public AnswerValue AnswerValue { get; set; }
        public Guid TestRunId { get; set; }

        public AnswerDTO()
        {
            Id = Guid.NewGuid();
        }

        public AnswerDTO(AnswerValue answerValue, Guid id, Guid testRunId)
        {
            Id = id;
            AnswerValue = answerValue;
            TestRunId = testRunId;
        }
    }
}
