using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Teams.Domain
{
    public class Question : Entity
    {
        public string Text { get; protected set; }
        private List<TestQuestion> _testQuestions;
        public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.ToList();

        public Question(string text)
        {
            Text = text;
            _testQuestions = new List<TestQuestion>();
        }
    }
}
