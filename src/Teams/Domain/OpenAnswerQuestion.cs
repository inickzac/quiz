using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Domain
{
    public class OpenAnswerQuestion : Question
    {
    
        public string Answer { get; private set; }

        public OpenAnswerQuestion(string text, string answer) : base(text)
        {
            Answer = answer;
        }
        public bool CheckAnswer(string answer)
        {
            answer = answer.Trim();
            if (answer == Answer)
                return true;
            return false; 
        }

    }
}
