using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain.DTO_Models
{
    public class TestDTO
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public List<QuestionDTO> Questions { get; set; }
        public TestDTO()
        {
            Questions = new List<QuestionDTO>();
        }
}
}
