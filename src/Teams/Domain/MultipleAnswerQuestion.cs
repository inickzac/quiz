using System;
using System.Collections.Generic;
using Teams.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Net.Mime;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teams.Domain
{
    public class MultipleAnswerQuestion : Question
    {
        List<MultipleAnswerQuestionOption> answers;
        public List<int> ChosenOptions;
        public IReadOnlyCollection<MultipleAnswerQuestionOption> Answers { get; set; }
        public MultipleAnswerQuestion(string text) : base(text)
        {
        }
        public MultipleAnswerQuestion(string text, List<MultipleAnswerQuestionOption> answers) : base(text)
        {
            if (answers.Count == 0) throw new ArgumentException("A question must have at least one possible answer");
            this.answers = answers;
            Answers = answers.ToList();
        }
        public bool[] GetRightAnswers()
        {
            bool[] right = new bool[Answers.Count];
            var listOfAnswers = Answers.ToList();
            for (int i = 0; i < right.Length; i++)
            {
                right[i] = listOfAnswers[i].IsRight;
            }
            return right;
        }
    }
}
