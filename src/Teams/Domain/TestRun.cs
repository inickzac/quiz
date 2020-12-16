using System;
using System.Collections.Generic;

namespace Teams.Domain
{
    /// <summary>
    ///     The entity keeps track of a test run for a specific user. Contains the IDs for the current user and of the Test.
    /// </summary>
    public class TestRun : Entity
    {
        public string TestedUserId { get; }
        public Guid TestId { get; }
        public List<Answer> Answers { get; private set; }
        public bool InProgress { get; set; }
        
        public TestRun(string testedUserId, Test test, List<Guid> testQuestionIds)
        {
            TestedUserId = testedUserId;
            TestId = test.Id;
            InProgress = true;
        }

        public TestRun()
        {
        }
    }
}