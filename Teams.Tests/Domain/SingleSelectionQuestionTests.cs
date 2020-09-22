using System;
using System.Collections.Generic;
using System.Text;
using Teams.Domain;
using Xunit;

namespace Teams.Tests.Domain
{
    public class SingleSelectionQuestionTests
    {
        [Fact]
        public void GetRightAnswer_ReturnsTrueOption()
        {
            //Arrange
            var theTrueOption = new SingleSelectionQuestionOption("2nd option (true)", true);
            var options = new List<SingleSelectionQuestionOption>()
            {
                new SingleSelectionQuestionOption( "1st option (false)", false),
                theTrueOption,
                new SingleSelectionQuestionOption("3rd option (false)", false),
                new SingleSelectionQuestionOption("4th option (false)", false)
            };
            var singleSelectionQuestion = new SingleSelectionQuestion("Question text");
            singleSelectionQuestion.InitializeOptions(options);

            //Act
            var answer = singleSelectionQuestion.GetRightAnswer();

            //Assert
            Assert.True(answer.IsAnswer);
            Assert.Equal(theTrueOption.Text, answer.Text);
        }
    }
}
