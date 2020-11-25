using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Models
{
    public class MAQDTOModel  //Вот моя DTO модель
    {
        public string questionText { get; set; }
        public string[] jTextList { get; set; }
        public bool[] jCheckboxList { get; set; }

    }
}

