using System.Collections.Generic;

namespace Teams.Domain
{
    public class SingleSelectionQuestion : Question
    {
        public List<QuestionOption> OptionList { get; private set; }
        public SingleSelectionQuestion(string text) : base(text)
        {
            OptionList = new List<QuestionOption>();
        }
        public SingleSelectionQuestion(string text, List<QuestionOption> options): base(text)
        {
            OptionList = options;
        }
        public void InitOptionList(List<QuestionOption> optionList)
        {
            OptionList = optionList;
        }
    }
}
