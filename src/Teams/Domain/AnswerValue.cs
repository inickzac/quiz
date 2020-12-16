using System.Collections.Generic;

namespace Teams.Domain
{
    /// <summary>
    ///     Stores answer text values. IsOption (true) means stored value is a Guid of the existing option, false means it is a
    ///     textual value.
    /// </summary>
    public class AnswerValue : Entity
    {
        public List<string> AnswerText { get; private set; }
        private bool IsOption { get; set; }

        public void AddAnswer(string answer, bool isOption)
        {
            AnswerText.Add(answer);
            IsOption = isOption;
        }

        public void AddAnswer(List<string> answer, bool isOption)
        {
            AnswerText = answer;
            IsOption = isOption;
        }
    }
}