using System;
using System.Collections.Generic;
using System.Text;
using Teams.Domain;
using Xunit;

namespace Teams.Tests.Domain
{
    public class OpenAnswerQuestionUpdateTests
    {
        [Fact]
        public void UpdateQuestion_ChangesTextAndAnswer()
        {
            //arrange

            string expectedQuestion = "What year was epam founded?";
            string expectedAnswer = "1993";

            //act

            OpenAnswerQuestion question = new OpenAnswerQuestion("", "");
            question.UpdateQuestion(expectedQuestion, expectedAnswer);

            //Assert

            Assert.Equal(expectedQuestion, question.Text);
            Assert.Equal(expectedAnswer, question.Answer);
        }
    }
}
