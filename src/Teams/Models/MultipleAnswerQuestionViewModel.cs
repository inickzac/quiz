using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class MultipleAnswerQuestionViewModel
    {
        public MultipleAnswerQuestion SourceQuestion { get; set; }
        public int[] ChosenOptions { get; set; }
    }
}
