using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    /// <summary>
    /// Clqass Answer contains answers to current Test question. Answer is stored as string. Multiple answers are delimited with /*/ special characters
    /// </summary>
    public class Answer : Entity
    {
        public string AnswerText {get; private set;};
        public TestRun TestRun { get; set; }
        public Guid TestRunFK { get; set; }

        public void SetAnswer(string answer)
        {
            AnswerText = answer;
        }

        public void SetAnswer(ICollection<string> answer)
        {
            foreach(var n in answer)
            AnswerText += "/*/" + n;
        }
    }
}
