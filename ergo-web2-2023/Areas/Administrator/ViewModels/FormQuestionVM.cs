namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class FormQuestionVM
    {
        public int FormId { get; set; }
        public string? Title { get; set; }
        public int QuestionId { get; set; }
        public int? QuestionOrder { get; set; }
        public int? GroupId { get; set; }
    }
}
