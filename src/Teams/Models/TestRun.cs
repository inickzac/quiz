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
    /// The entity keeps track of a specific test run for a specific user. Contains the IDs of the current user and of the Test.
    /// </summary>
    public class TestRun : Entity
    {
        /// <summary>
        /// Stores the ID of the current TestRun user
        /// </summary>
        [ForeignKey("TestedUserID_FK")] public string TestedUserId { get; set; }
        /// <summary>
        /// Stores the ID of the specified test
        /// </summary>
        [ForeignKey("TestID_FK")] public Guid TestId { get; private set; }
        /// <summary>
        /// Specifies if the current test run instance is in progress. True for in progress, false for completed
        /// </summary>
        public bool InProgress { get; private set; }

        /// <summary>
        /// Stores values for questionId and answer.
        /// </summary>
        /// <summary>
        [ForeignKey("QuestionAnswerPairs_FK")]public List<QuestionAnswerPair> QuestionAnswerPairs { get; set; }
        /// <summary>
        /// Starts the test, sets a new test by its id, sets a new user by his/her id. Sets the InProgress field to true.
        /// </summary>
        /// <param name="testDtoId">Guid of the test that is added to the current Test Run</param>
        /// <param name="userUd">Guid of the user of the current Test Run</param>
        public void StartTestRun(Guid testDtoId, string userUd)
        {
            TestId = testDtoId;
            TestedUserId = userUd;
            InProgress = true;
        }
        /// <summary>
        /// Ends the current TestRun, sets the Inprogress value to false.
        /// </summary>
        public void EndTestRun()
        {
            InProgress = false;
        }

        public void AddAnswer(Guid questionId, Guid answerId)
        {
            QuestionAnswerPair questionAnswerPair = new QuestionAnswerPair {TestRun = this};
            questionAnswerPair.Add(questionId, answerId);
            QuestionAnswerPairs.Add(questionAnswerPair);
        }
    }
}
