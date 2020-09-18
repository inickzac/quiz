using System;
using System.Collections.Generic;
using Teams.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Net.Mime;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Teams.Domain
{
    public class MultipleAnswerQuestion : Question
    {
        public MultipleAnswerQuestion(string text) : base(text)
        {
        }
        public MultipleAnswerQuestion(string t, List<Answer> a) : base(t)
        {
            if (a.Count == 0) throw new ApplicationException("A question must have possible answers");
            answers = a;
            text = t;
        }
        public List<Answer> answers;
        public readonly string text;
        public static MultipleAnswerQuestion PickById(Guid id, ApplicationDbContext context)
        {
            dynamic q;
            using (var cont = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
            {
                q = from item in cont.MultipleAnswerQuestions
                        where item.Id == id
                        select item;
            }
            return q.First();
        }
    }
}
