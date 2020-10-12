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
            var answer = openAnswerQuestion.IsCorrectAnswer("Eton");

            //Assert
            Assert.True(answer);
        }
    }
}
