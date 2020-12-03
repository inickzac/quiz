using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class Answer : Entity
    {
        private string _simpleAnswer;
        private ICollection<string> _complexAnswer;

        public void SetAnswer(string answer)
        {
            _simpleAnswer = answer;
        }

        public void SetAnswer(ICollection<string> answer)
        {
            _complexAnswer = answer;
        }

        public object GetAnswer()
        {
            if (_simpleAnswer?.Length == 0) return _complexAnswer;
            return _simpleAnswer;
        }
    }
}
