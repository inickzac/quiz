using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class QuestionAnswer : Entity
    {
        public Guid QuestionId { get; private set; }
        public Guid AnswerId { get; private set; }
        [ForeignKey("TestRun_FK")] public TestRun TestRun { get; set; }


        public void Add(Guid questionId, Guid answerId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
        }
    }
}
