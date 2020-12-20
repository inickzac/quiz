using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<OpenAnswerQuestion> OpenAnswerQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SingleSelectionQuestion> SingleSelectionQuestions { get; set; }
        public DbSet<MultipleAnswerQuestion> MultipleAnswerQuestions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<ProgramCodeQuestion> ProgramCodeQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestRun> TestRuns { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TestRun>(
                b =>
                {
                    b.Property(e => e.TestedUserId);
                    b.Property(e => e.TestId);
                    b.HasMany(x => x.Answers).WithOne().IsRequired().HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
                    b.Metadata.FindNavigation("Answers").SetPropertyAccessMode(PropertyAccessMode.Field);
                });
            builder.Entity<Answer>(
                b =>
                {
                    b.Property(e => e.AnswerText);
                    b.Property(e => e.AnswerOptions).HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
                    //b.Property(e => e.Answers);
                    //b.HasMany(x => x.Answers).WithOne().IsRequired().HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
                    //b.Metadata.FindNavigation("Answers").SetPropertyAccessMode(PropertyAccessMode.Field);
                });
        }
    }
}
