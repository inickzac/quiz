using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Teams.Domain;
using Teams.Domain.DTO_Models;

namespace Teams.Models
{
    /// <summary>
    /// The entity keeps track of a test run for a specific user. Contains the IDs for the current user and of the Test.
    /// </summary>
    public class TestRun : Entity
    {
        
        
        public string TestedUserID { get; set; }
        public ApplicationUser TestedUser { get; set; }
        [ForeignKey("Test_FK")] public Guid TestId { get; set; }
        public List<Test> Tests {get; set;}
        public List<Answer> Answers {get; set;}
        public bool InProgress { get; private set; }

        public int AnswersCounter { get; private set; }

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
            AnswersCounter = 0;
            InProgress = false;
        }



        public void StartTestRun()
        {
            InProgress = true;
        }

        public void EndTestRun()
        {
            InProgress = false;
            AnswersCounter = 0;
        }

        public void AddAnswer(Guid questionId, Guid answerId)
        {
            AnswersCounter++;
        }
    }
}
