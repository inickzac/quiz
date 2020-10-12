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
        public bool IsCorrectAnswer(string answer)
        {            
            answer = answer.Trim();
            return answer == Answer;              
        }


    }
}
