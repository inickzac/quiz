using System;
using System.Collections.Generic;
using System.Linq;

namespace Teams.Domain
{
    public class SingleSelectionQuestion : Question
    {
        private List<SingleSelectionQuestionOption> options;
        public IReadOnlyCollection<SingleSelectionQuestionOption> Options => options.ToList();
        public SingleSelectionQuestion(string text) : base(text)
        {
            options = new List<SingleSelectionQuestionOption>();
        }
        public SingleSelectionQuestionOption GetRightAnswer()
        {
            var answer = this.Options.FirstOrDefault(i => i.IsAnswer == true);
            return answer;
        }
    }
}
