using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Models
{
    public class MAQuestionModel
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public string ID { get; set; }
        public List<int> ChosenOptions { get; set; }
    }
}
