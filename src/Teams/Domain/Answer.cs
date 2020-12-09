using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teams.Domain
{
    /// <summary>
    /// Class Answer contains answers to current Test question. Answer is stored as string. Multiple answers are delimited with /*/ special characters
    /// </summary>
    public class Answer : Entity
    {
        [NotMapped]
        public List<string> AnswerText {get; private set;}
        public TestRun TestRun { get; set; }
        public Guid TestRunFK { get; set; }

        public void SetAnswer(string answer)
        {
            AnswerText.Add(answer);
        }

        public void SetAnswer(List<string> answer)
        {
            foreach(var n in answer) AnswerText.Add(n);
        }
    }
}
