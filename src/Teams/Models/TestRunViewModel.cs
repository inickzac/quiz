using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class TestRunViewModel : Entity
    {
        public List<Answer> Answers { get; set; }
        public List<Question> Questions { get; set; }

        public TestRunViewModel(List<Answer> answers, List<Question> questions)
        {
            Answers = answers;
            Questions = questions;
        }
    }
}
