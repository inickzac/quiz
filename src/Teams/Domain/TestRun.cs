using System;
using System.Collections.Generic;
using Teams.Models;

namespace Teams.Domain
{
    /// <summary>
    /// The entity keeps track of a test run for a specific user. Contains the IDs for the current user and of the Test.
    /// </summary>
    public class TestRun : Entity
    {
        public string TestedUserID { get; private set; }
        public Guid TestId { get; private set; }
        public List<Guid> TestQuestionIds { get; private set; }
        public List<Guid> AnswerIds { get; private set; }
        public List<Answer> Answers { get; private set; }
        public bool InProgress { get; set; }

        public TestRun(string testedUserId, Test test, List<Guid> testQuestionIds)
        {
            TestedUserID = testedUserId;
            TestId = test.Id;
            InProgress = true;
            TestQuestionIds = testQuestionIds;
        }

        public void AddAnswerId(Guid answerId)
        {
            AnswerIds.Add(answerId);
        }

        public TestRun()
        {
        }
    }
}
