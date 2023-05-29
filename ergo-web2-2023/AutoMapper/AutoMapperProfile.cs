using AutoMapper;

using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Areas.Administrator.ViewModels;
using ergo_web2_2023.ViewModels;
using FormVM = ergo_web2_2023.Areas.Administrator.ViewModels.FormVM;

namespace ergo_web2_2023.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Question, QuestionVM>();
            CreateMap<QuestionVM, Question>();

            CreateMap<FormVM, Form>();
            CreateMap<Form, FormVM>();

            CreateMap<Question, QuestionEditVM>();
            CreateMap<QuestionEditVM, Question>();

            CreateMap<Subquestion, SubquestionVM>().ForMember(dest => dest.SubQuestionTitle, opt => opt.MapFrom(src => src.SubQuestion.Title))
                .ForMember(dest => dest.QuestionTitle, opt => opt.MapFrom(src => src.Question.Title));
            CreateMap<SubquestionVM, Subquestion>();

            CreateMap<Subquestion, SubquestionEditVM>();
            CreateMap<SubquestionEditVM, Subquestion>();

            CreateMap<Subquestion, SubquestionAddVM>();
            CreateMap<SubquestionAddVM, Subquestion>();

            CreateMap<Form, FormEditVM>();
            CreateMap<FormEditVM, Form>();

            CreateMap<FormQuestion, Areas.Administrator.ViewModels.FormQuestionVM>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Question.Title));
            CreateMap<Areas.Administrator.ViewModels.FormQuestionVM, FormQuestion>();

            CreateMap<Question, QuestionsVM>(); 


            CreateMap<QuestionOption, QuestionOptionsVM>();

            CreateMap<FormQuestion, ViewModels.FormQuestionVM>();

            CreateMap<ViewModels.FormQuestionVM, FormQuestion>();

            CreateMap<FormGroup, ViewModels.FormGroupVM>();
            CreateMap<ViewModels.FormGroupVM, FormGroup>();

            CreateMap<Subquestion, SubQuestionVM>();
            CreateMap<SubQuestionVM, Subquestion>();

            CreateMap<FormGroup, Areas.Administrator.ViewModels.FormGroupVM>();
            CreateMap<Areas.Administrator.ViewModels.FormGroupVM, FormGroup>();

            CreateMap<Form, ViewModels.FormVM>();
            CreateMap<ViewModels.FormVM, Form>();

            CreateMap<QuestionOption, OptionVM>();
            CreateMap<OptionVM, QuestionOption>();
        }
    }
}
