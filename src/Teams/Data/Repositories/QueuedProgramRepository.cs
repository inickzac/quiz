using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;
using Teams.Models;

namespace Teams.Data.Repositories
{
    public class QueuedProgramRepository : IQueuedProgramRepository
    {
        private ApplicationDbContext _db;
        public QueuedProgramRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Guid id, string text)
        {
            var model = new QueuedProgram(id, text);
            _db.QueuedPrograms.Add(model);
        }
    }
}
