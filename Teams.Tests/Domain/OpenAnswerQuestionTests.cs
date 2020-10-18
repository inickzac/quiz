using System;
using System.Collections.Generic;
using System.Text;
using Teams.Domain;
using Xunit;

namespace Teams.Tests.Domain
{
    
    public class OpenAnswerQuestionTests
    {
        [Fact]
        public void CheckAnswer_Return()
        {
            //Arrange
            var openAnswerQuestion = new OpenAnswerQuestion("What is the oldest public school in England?", "Eton");

            //Act
            var firstAnswer = openAnswerQuestion.IsCorrectAnswer("Eton");
            var secondAnswer = openAnswerQuestion.IsCorrectAnswer(" Eton");
            var thirdAnswer = openAnswerQuestion.IsCorrectAnswer("Eton ");
            var fourthAnswer = openAnswerQuestion.IsCorrectAnswer(" Eton ");

            //Assert
            Assert.True(firstAnswer);
            Assert.True(secondAnswer);
            Assert.True(thirdAnswer);
            Assert.True(fourthAnswer);
        }
        [Fact]
        public void CheckAnswer_Fail_Return()
        {
            //Arrange
            var openAnswerQuestion = new OpenAnswerQuestion("What is the oldest public school in England?", "Eton");

            //Act
            var answer = openAnswerQuestion.IsCorrectAnswer("Harrow");
         
            //Assert
            Assert.False(answer);
        }
    }
}
