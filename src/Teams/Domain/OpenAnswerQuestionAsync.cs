using System;
using Teams.Models;

namespace Teams.Domain
{
    public class OpenAnswerQuestionAsync : Question
    {
    
        public string Answer { get; private set; }

        public OpenAnswerQuestionAsync(string text, string answer) : base(text)
        {
            Answer = answer;
        }

        public OpenAnswerQuestionAsync(string text) : base(text)
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
