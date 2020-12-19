using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<SingleSelectionQuestion> SingleSelectionQuestions { get; set; }
        public DbSet<MultipleAnswerQuestion> MultipleAnswerQuestions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        int SaveChanges();
        public DbSet<OpenAnswerQuestion> OpenAnswerQuestions { get; set;}
        public DbSet<ProgramCodeQuestion> ProgramCodeQuestions { get; set; }
        public DbSet<QueuedProgram> QueuedPrograms { get; set; }
    }
}
