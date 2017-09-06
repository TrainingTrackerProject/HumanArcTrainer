using AutoMapper;
using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.ViewModels;

namespace HumanArc.Compliance.Web
{
    public class MappingConfig : Profile
    {
        protected override void Configure()
        {
            CreateMap<Training, TrainingViewModel>().ReverseMap();
            CreateMap<Question, QuestionViewModel>().ReverseMap();
            CreateMap<Answer, AnswerViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Training, TrainingSummaryViewModel>().ReverseMap();
            CreateMap<Schedule, ScheduleViewModel>().ReverseMap();
            CreateMap<ADGroup, ADGroupViewModel>().ReverseMap();
        }
    }
}
