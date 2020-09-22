using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain
{
    public interface ISingleSelectionQuestion
    {
        SingleSelectionQuestionOption Get(Guid id);
    }
}
