namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class FormDetailVM
    {
        public FormVM Form { get; set; }

        public IEnumerable<FormQuestionVM> FormQuestions { get; set; }

        public IEnumerable<QuestionVM> Questions { get; set; }
    }
}
