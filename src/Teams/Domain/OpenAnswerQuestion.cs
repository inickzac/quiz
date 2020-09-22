using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teams.Domain;

namespace Teams.Domain
{
    public class OpenAnswerQuestion : Entity
    {

        public string TextQuestion { get; private set; }
        public string TextAnswer { get; private set; }

        public OpenAnswerQuestion(string textQuestion, string textAnswer)
        {
            TextQuestion = textQuestion;
            TextAnswer = textAnswer;
        }

    }
}
