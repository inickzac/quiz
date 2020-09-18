using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public class Answer : Entity
    {
        public Answer(string t, bool r = false, Question p = null)
        {
            parent = p;
            text = t;
            isRight = r;
        }
        public Question parent;
        public string text;
        public bool isRight;
    }
}
