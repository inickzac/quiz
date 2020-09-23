using System;
using System.Collections.Generic;
using System.Linq;

namespace Teams.Domain
{
    public class SingleSelectionQuestion : Question
    {
        private List<SingleSelectionQuestionOption> _options;
        public IReadOnlyCollection<SingleSelectionQuestionOption> Options => _options.ToList();
        public SingleSelectionQuestion(string text) : base(text)
        {
            _options = new List<SingleSelectionQuestionOption>();
        }
        public SingleSelectionQuestionOption GetRightAnswer()
        {
            var answer = this.Options.FirstOrDefault(i => i.IsAnswer == true);
            return answer;
        }
        public void InitializeOptions(IEnumerable<SingleSelectionQuestionOption> options)
        {
            if (options.Where(x => x.IsAnswer).Count() != 1)
            {
                throw new ArgumentException();
            }
            _options = options.ToList();
        }
    }
}
