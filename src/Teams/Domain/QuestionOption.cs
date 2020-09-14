using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public class QuestionOption
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public bool IsAnswer { get; private set; }
        public QuestionOption(string text, bool isAnswer)
        {
            Text = text;
            IsAnswer = isAnswer;
        }
        public QuestionOption(int id, string text, bool isAnswer)
        {
            Id = id;
            Text = text;
            IsAnswer = isAnswer;
        }
    }
}
