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
        
        
        public string TestedUserID { get; set; }
        public ApplicationUser TestedUser { get; set; }
        public Guid TestId { get; set; }
        public List<Test> Tests {get; set;}
        public List<Answer> Answers {get; set;}
        public bool InProgress { get; set; }

        public int AnswersCounter { get; set; }

        public TestRun(ApplicationUser testedUser, Test test)
        {
            TestedUserID = testedUser.Id;
            TestedUser = testedUser;
            TestId = test.Id;
            AnswersCounter = 0;
            InProgress = true;
        }

        public TestRun()
        {
        }
    }
}
