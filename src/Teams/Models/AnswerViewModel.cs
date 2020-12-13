using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class AnswerViewModel : Entity
    {
        public List<string> AnswerTexts { get; set; }
        public TestQuestion TestQuestion { get; set; }
        public Guid TestRunGuid { get; set; }
    }
}
