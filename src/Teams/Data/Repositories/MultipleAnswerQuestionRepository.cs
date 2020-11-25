using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Data.Repositories
{
    public class MultipleAnswerQuestionRepository : IMultipleAnswerQuestionRepository
    {
        ApplicationDbContext _db;
        public MultipleAnswerQuestionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public MultipleAnswerQuestion PickById(Guid Id)
        {
            return _db.MultipleAnswerQuestions.Include(a => a.Answers).Single(q => q.Id == Id);
        }

        public void MethodForAdd(MultipleAnswerQuestion question)
        {
            _db.MultipleAnswerQuestions.Add(question);
            _db.SaveChanges();
        }

        //public void MethodForEdit(MultipleAnswerQuestion question)
        //{
        //    var item = _db.MultipleAnswerQuestions.Find(question.Id);

        //    if (item != null)
        //    {
        //        item.Text = question.Text;
        //    }


        //    var sprint = await _db.Sprint.FindAsync(entity.Id);
        //    if (sprint == null)
        //        return false;

        //    sprint.Name = entity.Name;
        //    sprint.StoryPointInHours = entity.StoryPointInHours;
        //    sprint.DaysInSprint = entity.DaysInSprint;
        //    sprint.IsActive = entity.IsActive;
        //    var result = _dbContext.Entry(sprint).State == EntityState.Modified ? true : false;
        //    await _dbContext.SaveChangesAsync();
        //}

    }
}