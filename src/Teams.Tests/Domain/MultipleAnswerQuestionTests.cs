using System.Collections.Generic;
using Xunit;
using Teams.Domain;
using Teams.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using System.Linq;
using System;
using Xunit.Extensions;

namespace Teams.Tests
{
    public class MultipleAnswerQuestionTests
    {
        MultipleAnswerQuestion _question = new MultipleAnswerQuestion("Choose correct answers", new List<MultipleAnswerQuestionOption>()
        {
            new MultipleAnswerQuestionOption("false"), new MultipleAnswerQuestionOption("correct", true), new MultipleAnswerQuestionOption("true", true),
            new MultipleAnswerQuestionOption("incorrect"), new MultipleAnswerQuestionOption("right", true)
        });
        [Fact]
        public void GetRightAnswers_CorrectnessTest()
        {
            Assert.True(_question.GetRightAnswers().All(item => item.IsRight)
                && _question.Answers.Where(a => a.IsRight).Count() == _question.GetRightAnswers().Length);
        }
        [Fact]
        public void GetRightAnswersIds_Test()
        {
            Assert.True(_question.GetRightAnswersIds().Length == _question.Answers.Where(a => a.IsRight).ToList().Count);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MultipleAnswerQuestion_EmptyAnswersArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var question = new MultipleAnswerQuestion(value, new List<MultipleAnswerQuestionOption>() { new MultipleAnswerQuestionOption("option")});
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var question = new MultipleAnswerQuestion(value);
            });
            Assert.Throws<ArgumentException>(() =>
            { 
                var question = new MultipleAnswerQuestion("Just a question", new List<MultipleAnswerQuestionOption>()); 
            });
        }
    }
}
