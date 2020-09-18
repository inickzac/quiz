using System.Collections.Generic;
using System.Linq;

namespace Teams.Domain
{
    public class SingleSelectionQuestion : Question
    {
        public List<QuestionOption> OptionList { get; private set; }
        public SingleSelectionQuestion(string text) : base(text)
        {
            OptionList = new List<QuestionOption>();
        }
       
    }
}
