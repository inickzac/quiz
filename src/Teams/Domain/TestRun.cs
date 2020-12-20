using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teams.Domain
{
    public class TestRun : Entity
    {
        public string TestedUserId { get; }
        public Guid TestId { get; }
        public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();
        private readonly List<Answer> _answers;
        public bool InProgress { get; private set; }
        
        public TestRun(string testedUserId, Guid testId, List<Answer> answers)
        {
            TestedUserId = testedUserId;
            TestId = testId;
            InProgress = true;
            _answers = answers;
        }

        public TestRun()
        {
        }

        public void Finish()
        {
            InProgress = false;
        }
    }
}