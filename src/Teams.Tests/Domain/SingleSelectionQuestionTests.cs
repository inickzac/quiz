using System;
using System.Collections.Generic;
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

        [Fact]
        public void InitializeOptions_GetsNoTrue_ArgumentException()
        {
            //Arrange
            var options = new List<SingleSelectionQuestionOption>()
            {
                new SingleSelectionQuestionOption("1st option (false)", false),
                new SingleSelectionQuestionOption("2nd option (false)", false)
            };
            var singleSelectionQuestion = new SingleSelectionQuestion("Question text");
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => singleSelectionQuestion.InitializeOptions(options));
        }

        [Fact]
        public void InitializeOptions_GetsManyTrue_ArgumentException()
        {
            //Arrange
            var options = new List<SingleSelectionQuestionOption>()
            {
                new SingleSelectionQuestionOption("1st option (true)", true),
                new SingleSelectionQuestionOption("2nd option (true)", true),
                new SingleSelectionQuestionOption("3nd option (false)", false)
            };
            var singleSelectionQuestion = new SingleSelectionQuestion("Question text");
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => singleSelectionQuestion.InitializeOptions(options));
        }
        [Fact]
        public void GetRightAnswer_GetsZeroOptions_ReturnsNull()
        {
            //Arrange
            var singleSelectionQuestion = new SingleSelectionQuestion("Question text");
            //Act
            var answer = singleSelectionQuestion.GetRightAnswer();
            //Assert
            Assert.Null(answer);
        }
    }
}
