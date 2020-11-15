using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Domain.DTO_Models
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public QuestionDTO()
        {
            Id = default;
            Text = default;
        }
        public QuestionDTO(Guid id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
