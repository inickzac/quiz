using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Models;

namespace Teams.Domain
{
    public class Test : Entity
    {
        public string Title { get; private set; }
        private List<TestQuestion> _testQuestions;
        public IReadOnlyCollection<TestQuestion> TestQuestions => _testQuestions.ToList();
        public Test(Guid id, string title, List<TestQuestion> questions) : base(id)
        {
            Title = title;
            _testQuestions = questions;
        }
        public Test(string title)
        {
            Title = title;
            _testQuestions = new List<TestQuestion>();
        }
    }
}
