using AutoMapper;
using Teams.Domain.DTO_Models;

namespace Teams.Domain
{
    public static class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<Test, TestDTO>();
            CreateMap<TestDTO, Test>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionDTO, Question>();
        }
    }
}