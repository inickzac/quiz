using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Models
{
    public class MultipleAnswerQuestionDTOModel  
    {
        public string QuestionText { get; set; }
        public string[] TextMassive { get; set; }
        public bool[] CheckboxValueMassive { get; set; }

    }
}

