using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public class SingleSelectionQuestionOption: Entity
    {
        public string Text { get; private set; }
        public bool IsAnswer { get; private set; }
        public SingleSelectionQuestionOption(string text, bool isAnswer): base()
        {
            Text = text;
            IsAnswer = isAnswer;
        }
    }
}
