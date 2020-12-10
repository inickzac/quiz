using System;
using Teams.Models;

namespace Teams.Domain
{
    public class OpenAnswerQuestion : Question
    {
    
        public string Answer { get; private set; }

        public OpenAnswerQuestion(string text, string answer) : base(text)
        {
            Answer = answer;
        }

        public OpenAnswerQuestion(string text) : base(text)
        {
        }

        public bool IsCorrectAnswer(string answer)
        {            
            answer = answer.Trim();
            return answer == Answer;              
        }

        public void UpdateQuestion(string question, string answer)
        {
            Text = question;
            Answer = answer;
        }
    }
}
