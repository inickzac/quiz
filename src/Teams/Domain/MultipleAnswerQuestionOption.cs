using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public class MultipleAnswerQuestionOption : Entity
    {
        public string Text { get; private set; }
        public bool IsRight { get; private set; }
        public MultipleAnswerQuestionOption(string text, bool isRight = false)
        {
            Text = text;
            IsRight = isRight;
        }
    }
}
