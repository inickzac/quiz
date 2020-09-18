using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public class QuestionOption: Entity
    {
        public string Text { get; private set; }
        public bool IsAnswer { get; private set; }
        public QuestionOption(string text, bool isAnswer): base()
        {
            Text = text;
            IsAnswer = isAnswer;
        }
    }
}
