using System;
using System.Collections.Generic;
using System.Text;
using Teams.Domain;
using Xunit;

namespace Teams.Tests.Domain
{

    public class OpenAnswerQuestionTests
    {
        [Theory]
        [InlineData("Eton")]
        [InlineData(" Eton")]
        [InlineData("Eton ")]
        [InlineData(" Eton ")]
        public void CheckAnswer_Return(string textAnswer)
        {
            //Arrange
            var openAnswerQuestion = new OpenAnswerQuestion("What is the oldest public school in England?", "Eton");

            //Act
            var answer = openAnswerQuestion.IsCorrectAnswer(textAnswer);
           

            //Assert
            Assert.True(answer);
            
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
