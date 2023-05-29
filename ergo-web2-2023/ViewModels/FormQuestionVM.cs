using ergo_web2_2023.ViewModels;

namespace ergo_web2_2023.ViewModels
{
    public class FormQuestionVM
    {
        public int FormId { get; set; }
        public int GroupId { get; set; }
        public int? QuestionOrder { get; set; }
        public QuestionsVM Question { get; set; }
        public FormGroupVM Group { get; set; }
    }
}
